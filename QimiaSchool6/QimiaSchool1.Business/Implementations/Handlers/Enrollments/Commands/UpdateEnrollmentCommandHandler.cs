using MediatR;
using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.Business.Implementations.Commands.Enrollments;
using QimiaSchool1.Business.Implementations.Events.Enrollments;
using QimiaSchool1.DataAccess.Exceptions;
using QimiaSchool1.DataAccess.MessageBroker.Abstractions;

namespace QimiaSchool1.Business.Implementations.Handlers.Enrollments.Commands;

public class UpdateEnrollmentCommandHandler : IRequestHandler<UpdateEnrollmentCommand, Unit>
{
    private readonly IEnrollmentManager _enrollmentManager;
    private readonly IEventBus _eventBus;

    public UpdateEnrollmentCommandHandler(IEnrollmentManager enrollmentManager, IEventBus eventBus)
    {
        _enrollmentManager = enrollmentManager;
        _eventBus = eventBus;
    }
    public async Task<Unit> Handle(UpdateEnrollmentCommand request, CancellationToken cancellationToken)
    {

        var existingEnrollment = await _enrollmentManager.GetEnrollmentByIdAsync(request.EnrollmentId, cancellationToken);

        existingEnrollment.CourseId = request.Enrollment.CourseId;
        existingEnrollment.StudentId = request.Enrollment.StudentId;

        await _enrollmentManager.UpdateEnrollmentAsync(existingEnrollment, cancellationToken);

        await _eventBus.PublishAsync(new EnrollmentUpdatedEvent
        {
            EnrollmentId = existingEnrollment.EnrollmentId,
            CourseId = existingEnrollment.CourseId,
            StudentId = existingEnrollment.StudentId,
        });

        return Unit.Value;
    }
}
