using MediatR;

namespace QimiaSchool1.Business.Implementations.Commands.Students;

public class DeleteStudentCommand : IRequest<Unit>
{
    
    public int StudentId { get;}

    public DeleteStudentCommand(int studentId)
    {
        StudentId = studentId;
    }
}
