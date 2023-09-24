using MediatR;
using QimiaSchool1.Business.Implementations.Commands.Enrollments.Dtos;

namespace QimiaSchool1.Business.Implementations.Commands.Enrollments;

public class CreateEnrollmentCommand : IRequest<int>
{
    public CreateEnrollmentDto Enrollment { get; set; }

    public CreateEnrollmentCommand(CreateEnrollmentDto enrollment)
    {
        Enrollment = enrollment;
    }
}
