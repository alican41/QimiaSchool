

using MassTransit;
using Microsoft.Extensions.Logging;

namespace QimiaSchool1.Business.Implementations.Events.Courses;

public class CourseUpdatedEventConsumer : IConsumer<CourseUpdatedEvent>
{
    private readonly ILogger<CourseUpdatedEventConsumer> _logger;

    public CourseUpdatedEventConsumer(ILogger<CourseUpdatedEventConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<CourseUpdatedEvent> context)
    {
        _logger.LogInformation("Course updated: {@Course}", context.Message);

        return Task.CompletedTask;
    }
}
