using MediatR;
using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.Business.Implementations.Commands.Students;


namespace QimiaSchool1.Business.Implementations.Handlers.Students.Commands;

public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, Unit>
{
    private readonly IStudentManager _studentManager;

    public DeleteStudentCommandHandler(IStudentManager studentManager)
    {
        _studentManager = studentManager;
    }

    public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            

            await _studentManager.DeleteStudentByIdAsync(request.StudentId, cancellationToken);

            return Unit.Value;
        }
        catch (Exception ex)
        {
            //entity arg invalid 
            
        }
        return Unit.Value;
    }


}
