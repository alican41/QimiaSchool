using AutoMapper;
using QimiaSchool1.Business.Implementations.Queries.Course.Dtos;
using QimiaSchool1.Business.Implementations.Queries.Enrollment.Dtos;
using QimiaSchool1.Business.Implementations.Queries.Student.Dtos;
using QimiaSchool1.DataAccess.Entities;


namespace QimiaSchool1.Business.Implementations.MapperProfiles;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Enrollment, EnrollmentDto>()
            ;
        CreateMap<Student, StudentDto>()
            .ForMember(dest => dest.Enrollments, opt => opt.MapFrom(src => src.Enrollments))
            ;
        CreateMap<Course, CourseDto>()
            ;
    }
}

