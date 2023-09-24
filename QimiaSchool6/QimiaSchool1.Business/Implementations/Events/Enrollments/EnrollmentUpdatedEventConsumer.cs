using MassTransit;
using Microsoft.Extensions.Logging;

namespace QimiaSchool1.Business.Implementations.Events.Enrollments;

public class EnrollmentUpdatedEventConsumer : IConsumer<EnrollmentUpdatedEvent>
{
    private readonly ILogger<EnrollmentUpdatedEventConsumer> _logger;

    public EnrollmentUpdatedEventConsumer(ILogger<EnrollmentUpdatedEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<EnrollmentUpdatedEvent> context)
    {
        _logger.LogInformation("Enrollment updated: {@Enrollment}", context.Message);

        return Task.CompletedTask;
    }
}
