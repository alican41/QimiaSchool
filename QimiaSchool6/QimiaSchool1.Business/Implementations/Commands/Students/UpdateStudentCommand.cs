using MediatR;
using QimiaSchool1.Business.Implementations.Commands.Students.Dtos;

namespace QimiaSchool1.Business.Implementations.Commands.Students;

public class UpdateStudentCommand : IRequest<Unit>
{
    public UpdateStudentDto Student { get; set; }

    public int StudentId { get; }

    public UpdateStudentCommand(UpdateStudentDto updateStudent, int studentId)
    {
        Student = updateStudent;
        StudentId = studentId;
    }
}
