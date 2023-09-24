using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool1.Business.Implementations.Queries.Course.Dtos;

public class CourseDto
{
    public int CourseId { get; set; }
    public string? CourseTitle { get; set; } 
    public int CourseCredits { get; set; }

    public ICollection<EnrollmentManager>? Enrollments { get; set; }



}
