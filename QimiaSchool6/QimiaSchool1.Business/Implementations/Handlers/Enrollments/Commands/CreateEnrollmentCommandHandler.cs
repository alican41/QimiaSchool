using MediatR;
using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.Business.Implementations.Commands.Enrollments;
using QimiaSchool1.Business.Implementations.Events.Enrollments;
using QimiaSchool1.DataAccess.Entities;
using QimiaSchool1.DataAccess.MessageBroker.Abstractions;

namespace QimiaSchool1.Business.Implementations.Handlers.Enrollments.Commands;

public class CreateEnrollmentCommandHandler : IRequestHandler<CreateEnrollmentCommand, int>
{
    private readonly IEnrollmentManager _enrollmentManager;
    private readonly IEventBus _eventBus;

    public CreateEnrollmentCommandHandler(IEnrollmentManager enrollmentManager, IEventBus eventBus)
    {
        _enrollmentManager = enrollmentManager;
        _eventBus = eventBus;
    }

    public async Task<int> Handle(
        CreateEnrollmentCommand request,
        CancellationToken cancellationToken)
    {
        var enrollment = new Enrollment
        {
            StudentId = request.Enrollment.StudentId,
            CourseId = request.Enrollment.CourseId,
        };

        await _enrollmentManager.CreateEnrollmentAsync(enrollment, cancellationToken);

        await _eventBus.PublishAsync(new EnrollmentCreatedEvent
        {
            EnrollmentId = enrollment.EnrollmentId,
            StudentId = enrollment.StudentId,
            CourseId = enrollment.CourseId,
        });

        return enrollment.EnrollmentId;
    }
}
