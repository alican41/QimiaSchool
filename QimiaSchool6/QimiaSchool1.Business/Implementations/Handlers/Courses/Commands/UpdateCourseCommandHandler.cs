using MediatR;
using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.Business.Implementations.Commands.Courses;
using QimiaSchool1.Business.Implementations.Events.Courses;
using QimiaSchool1.DataAccess.Entities;
using QimiaSchool1.DataAccess.Exceptions;
using QimiaSchool1.DataAccess.MessageBroker.Abstractions;

namespace QimiaSchool1.Business.Implementations.Handlers.Courses.Commands;

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Unit>
{
    private readonly ICourseManager _courseManager;
    private readonly IEventBus _eventBus;

    public UpdateCourseCommandHandler(ICourseManager courseManager, IEventBus eventBus)
    {
        _courseManager = courseManager;
        _eventBus = eventBus;
    }

    public async Task<Unit> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        
        var existingCourse = await _courseManager.GetCourseByIdAsync(request.CourseId, cancellationToken);

        existingCourse.CourseTitle = request.Course.CourseTitle ?? existingCourse.CourseTitle;
        existingCourse.CourseCredits = request.Course.CourseCredits;

        await _courseManager.UpdateCourseAsync(existingCourse, cancellationToken);

        await _eventBus.PublishAsync(new CourseUpdatedEvent
        {
            CourseId = existingCourse.CourseId,
            CourseTitle = existingCourse.CourseTitle,
            CourseCredits = existingCourse.CourseCredits,
        });

        return Unit.Value;
    }
}
