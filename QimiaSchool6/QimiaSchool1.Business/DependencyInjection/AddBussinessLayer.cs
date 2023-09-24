using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.Business.Implementations;
using QimiaSchool1.Business.Implementations.MapperProfiles;
using System.Reflection;

namespace QimiaSchool1.Business.DependencyInjection;


public static class AddBussinessLayer
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {

        AddMediatRHandlers(services);
        AddManagers(services);
        AddAutoMapper(services);

        return services;
    }

    private static void AddMediatRHandlers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }

    private static void AddManagers(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICacheService, CacheService>();
        serviceCollection.AddScoped<IStudentManager, StudentManager>();
        serviceCollection.AddScoped<ICourseManager, CourseManager>();
        serviceCollection.AddScoped<IEnrollmentManager, EnrollmentManager>();
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MapperProfile));
    }
}