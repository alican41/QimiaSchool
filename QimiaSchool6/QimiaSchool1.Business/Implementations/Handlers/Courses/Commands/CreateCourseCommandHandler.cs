using MediatR;
using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.Business.Implementations.Commands.Courses;
using QimiaSchool1.Business.Implementations.Events.Courses;
using QimiaSchool1.DataAccess.Entities;
using QimiaSchool1.DataAccess.MessageBroker.Abstractions;

namespace QimiaSchool1.Business.Implementations.Handlers.Courses.Commands;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, int>
{
    private readonly ICourseManager _courseManager;
    private readonly IEventBus _eventBus;

    public CreateCourseCommandHandler(ICourseManager courseManager, IEventBus eventBus)
    {
        _courseManager = courseManager;
        _eventBus = eventBus;
    }

    public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new Course
        {
            CourseTitle = request.Course.CourseTitle ?? string.Empty,
            CourseCredits = request.Course.CourseCredits,
        };

        await _courseManager.CreateCourseAsync(course, cancellationToken);

        await _eventBus.PublishAsync(new CourseCreatedEvent
        {
            CourseId = course.CourseId,
            CourseTitle = course.CourseTitle,
            CourseCredits = course.CourseCredits,
        });


        return course.CourseId;
    }

}
