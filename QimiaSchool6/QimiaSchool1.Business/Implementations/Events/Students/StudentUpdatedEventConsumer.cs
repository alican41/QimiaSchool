
using MassTransit;
using Microsoft.Extensions.Logging;

namespace QimiaSchool1.Business.Implementations.Events.Students;

public class StudentUpdatedEventConsumer : IConsumer<StudentUpdatedEvent>
{
    private readonly ILogger<StudentUpdatedEventConsumer> _logger;

    public StudentUpdatedEventConsumer(ILogger<StudentUpdatedEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<StudentUpdatedEvent> context)
    {
        _logger.LogInformation("Student updated: {@Student}", context.Message);

        return Task.CompletedTask;
    }
}
