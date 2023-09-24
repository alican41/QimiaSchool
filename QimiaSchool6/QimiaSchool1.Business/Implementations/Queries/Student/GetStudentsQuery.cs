using MediatR;
using QimiaSchool1.Business.Implementations.Queries.Student.Dtos;

namespace QimiaSchool1.Business.Implementations.Queries.Student;

public class GetStudentsQuery : IRequest<List<StudentDto>>
{
}
