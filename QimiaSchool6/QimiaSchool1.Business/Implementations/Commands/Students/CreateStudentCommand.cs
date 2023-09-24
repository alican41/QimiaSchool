using MediatR;
using QimiaSchool1.Business.Implementations.Commands.Students.Dtos;


namespace QimiaSchool1.Business.Implementations.Commands.Students;

public class CreateStudentCommand : IRequest<int>
{
    public CreateStudentDto Student { get; set; }

    public CreateStudentCommand(
        CreateStudentDto student)
    {
        Student = student;
    }
}

