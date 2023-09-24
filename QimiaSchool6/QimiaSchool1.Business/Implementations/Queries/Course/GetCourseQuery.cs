using MediatR;
using QimiaSchool1.Business.Implementations.Queries.Course.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool1.Business.Implementations.Queries.Course;

public class GetCourseQuery : IRequest<CourseDto>
{
    public int CourseId { get; }

    public GetCourseQuery(int id) 
    {
        CourseId = id;
    }




}
