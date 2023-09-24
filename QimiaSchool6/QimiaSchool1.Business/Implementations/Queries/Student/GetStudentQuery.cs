using MediatR;
using QimiaSchool1.Business.Implementations.Queries.Student.Dtos;


namespace QimiaSchool1.Business.Implementations.Queries.Student;

public class GetStudentQuery : IRequest<StudentDto>
{
    public int Id { get; }

    public GetStudentQuery(int id)
    {
        Id = id;
    }
}
