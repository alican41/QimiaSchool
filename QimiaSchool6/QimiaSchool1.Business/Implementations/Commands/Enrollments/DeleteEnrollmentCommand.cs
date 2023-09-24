using MediatR;

namespace QimiaSchool1.Business.Implementations.Commands.Enrollments;

public class DeleteEnrollmentCommand : IRequest<Unit>
{
    public int EnrollmentId { get; }

    public DeleteEnrollmentCommand(int enrollmentId)
    {
        EnrollmentId = enrollmentId;
    }
}
