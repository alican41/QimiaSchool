using MediatR;
using QimiaSchool1.Business.Implementations.Queries.Course.Dtos;

namespace QimiaSchool1.Business.Implementations.Queries.Course;

public class GetCoursesQuery : IRequest<List<CourseDto>>
{
}
