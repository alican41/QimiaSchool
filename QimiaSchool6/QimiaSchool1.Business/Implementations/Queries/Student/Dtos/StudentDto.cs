using QimiaSchool1.Business.Implementations.Queries.Enrollment.Dtos;

namespace QimiaSchool1.Business.Implementations.Queries.Student.Dtos;

public class StudentDto
{
    public int StudentId { get; set; }
    public string? LastName { get; set; }
    public string? FirstMidName { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public IEnumerable<EnrollmentDto>? Enrollments { get; set; }

}
