using MediatR;
using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.Business.Implementations.Commands.Enrollments;

namespace QimiaSchool1.Business.Implementations.Handlers.Enrollments.Commands;

public class DeleteEnrollmentCommandHandler : IRequestHandler<DeleteEnrollmentCommand, Unit>
{
    private readonly IEnrollmentManager _enrollmentManager;

    public DeleteEnrollmentCommandHandler(IEnrollmentManager enrollmentManager)
    {
        _enrollmentManager = enrollmentManager;
    }

    public async Task<Unit> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _enrollmentManager.DeleteEnrollmentByIdAsync(request.EnrollmentId, cancellationToken);

            return Unit.Value;
        }
        catch (Exception ex)
        {

        }
        return Unit.Value;
    }
}
