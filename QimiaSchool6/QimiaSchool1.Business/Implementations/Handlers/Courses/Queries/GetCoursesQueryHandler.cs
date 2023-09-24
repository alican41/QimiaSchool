using AutoMapper;
using MediatR;
using QimiaSchool1.Business.Abstracts;
using QimiaSchool1.Business.Implementations.Queries.Course;
using QimiaSchool1.Business.Implementations.Queries.Course.Dtos;


namespace QimiaSchool1.Business.Implementations.Handlers.Courses.Queries;

public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, List<CourseDto>>
{
    private readonly IMapper _mapper;
    private readonly ICourseManager _courseManager;

    public GetCoursesQueryHandler(IMapper mapper, ICourseManager courseManager)
    {
        _mapper = mapper;
        _courseManager = courseManager;
    }

    public async Task<List<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _courseManager.GetAllCoursesAsync(cancellationToken);
        
        var courseDtos = _mapper.Map<List<CourseDto>>(courses);

        return courseDtos;
        
        
    }
}
