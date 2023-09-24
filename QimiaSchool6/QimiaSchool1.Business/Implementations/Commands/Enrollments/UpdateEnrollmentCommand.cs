using MediatR;
using QimiaSchool1.Business.Implementations.Commands.Enrollments.Dtos;

namespace QimiaSchool1.Business.Implementations.Commands.Enrollments;

public class UpdateEnrollmentCommand : IRequest<Unit>
{
    public UpdateEnrollmentDto Enrollment { get; set; }

    public int EnrollmentId { get; }

    public UpdateEnrollmentCommand(UpdateEnrollmentDto enrollment, int enrollmentId)
    {
        Enrollment = enrollment;
        EnrollmentId = enrollmentId;
    }
}
