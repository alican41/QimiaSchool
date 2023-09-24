using AutoMapper;
using MediatR;
using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.Business.Implementations.Queries.Course;
using QimiaSchool1.Business.Implementations.Queries.Course.Dtos;

namespace QimiaSchool1.Business.Implementations.Handlers.Courses.Queries;

public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, CourseDto>
{
    private readonly ICourseManager _courseManager;
    private readonly IMapper _mapper;

    public GetCourseQueryHandler(
        ICourseManager courseManager,
        IMapper mapper)
    {
        _courseManager = courseManager;
        _mapper = mapper;
    }

    public async Task<CourseDto> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseManager.GetCourseByIdAsync(request.CourseId, cancellationToken);
        return _mapper.Map<CourseDto>(course);
    }
}
