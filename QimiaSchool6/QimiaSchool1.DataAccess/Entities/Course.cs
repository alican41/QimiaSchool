using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QimiaSchool1.DataAccess.Entities;

public class Course
{
    public int CourseId { get; set; }
    public string CourseTitle { get; set; } = string.Empty;
    public int CourseCredits { get; set; }

    public ICollection<Enrollment>? Enrollments { get; set; }

}
