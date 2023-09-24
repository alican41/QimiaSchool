using Microsoft.EntityFrameworkCore;
using QimiaSchool1.DataAccess;
using QimiaSchool1.DataAccess.Repositories.Abstractions;
using QimiaSchool1.DataAccess.Repositories.Implementations;
using QimiaSchool1.Business.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using Serilog.Formatting.Elasticsearch;
using MassTransit;
using Microsoft.Extensions.Options;
using QimiaSchool1.DataAccess.MessageBroker.Implementations;
using QimiaSchool1.Business.Implementations.Events.Courses;
using QimiaSchool1.DataAccess.MessageBroker.Abstractions;
using QimiaSchool1.Common;
using System.Text;
using QimiaSchool1.Business.Middleware;
using QimiaSchool1.Business.Implementations.Events.Students;
using QimiaSchool1.Business.Implementations.Events.Enrollments;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "QimiaSchool1")
    .WriteTo.File(path: "logs/log.txt")
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
    {
        IndexFormat = "dotnetLog",
        AutoRegisterTemplate = true,
        MinimumLogEventLevel = LogEventLevel.Information,
        CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
    })
    .MinimumLevel.Verbose()
    .CreateLogger();

Log.Information("Application starting...");

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "qimiaschool3";
});


builder.Services.AddDbContext<QimiaSchoolDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), options => options.CommandTimeout(120)));

builder.Services.Configure<MessageBrokerSettings>(
    builder.Configuration.GetSection("MessageBroker"));

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<CourseCreatedEventConsumer>();
    busConfigurator.AddConsumer<CourseUpdatedEventConsumer>();
    busConfigurator.AddConsumer<StudentCreatedEventConsumer>();
    busConfigurator.AddConsumer<StudentUpdatedEventConsumer>();
    busConfigurator.AddConsumer<EnrollmentCreatedEventConsumer>();
    busConfigurator.AddConsumer<EnrollmentUpdatedEventConsumer>();

    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();

        configurator.Host(new Uri(settings.Host), h =>
        {
            h.Username(settings.UserName);
            h.Password(settings.Password);
        });

        configurator.ConfigureEndpoints(context);
    });
});




builder.Services.AddScoped<IStudentRepository, StudentRepository>();//dataacsses!
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
builder.Services.AddScoped<IEventBus,  EventBus>();
builder.Services.AddBusinessLayer();



builder.Services.Configure<Auth0Configuration>(builder.Configuration.GetSection("Auth0"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Auth0:ClientSecret"]));
    options.Authority = $"{builder.Configuration["Auth0:Domain"]}";
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = "name",
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        ValidIssuer = $"https://{builder.Configuration["Auth0:Domain"]}",
        ValidAudience = builder.Configuration["Auth0:Audience"],
    };
});




builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Qimia School", Version = "v1" });

    var securityScheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
        Scheme = "bearer",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    var securityRequirement = new OpenApiSecurityRequirement
    {
        { securityScheme, new[] { "Bearer" } }
    };
    c.AddSecurityRequirement(securityRequirement);
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<TokenRefreshMiddleware>();

app.MapControllers();

app.Run();

public partial class Program { }