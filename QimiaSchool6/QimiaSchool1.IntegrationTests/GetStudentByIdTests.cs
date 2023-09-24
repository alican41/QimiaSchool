using FluentAssertions;
using Newtonsoft.Json;
using QimiaSchool1.Business.Implementations.Queries.Student.Dtos;
using QimiaSchool1.DataAccess.Entities;
using System.Net;


namespace QimiaSchool1.IntegrationTests;

internal class GetStudentByIdTests : IntegrationTestBase
{
    public GetStudentByIdTests() : base()
    {

    }

    [Test]
    public async Task GetStudentById_WhenCalled_ReturnsCorrectStudent()
    {
        // Arrange
        var studentList = new List<Student>()
        {
            new ()
            {
                EnrollmentDate = DateTime.Now,
                FirstMidName = "deneme",
                LastName = "deneme",
            },
            new ()
            {
                EnrollmentDate = DateTime.Now,
                FirstMidName = "test",
                LastName = "test",
            }
        };

        databaseContext.Students.AddRange(studentList);
        await databaseContext.SaveChangesAsync();

        // Act
        var response = await client.GetAsync("/students/" + studentList[0].StudentId);
        var result = await response.Content.ReadAsStringAsync();
        var responseStudent = JsonConvert.DeserializeObject<StudentDto>(result);

        // Assert
        responseStudent
            .Should()
            .BeEquivalentTo(studentList[0],
                options => options
                    .Excluding(s => s.EnrollmentDate)
                    .Excluding(s => s.Enrollments));
    }

    [Test]
    public async Task GetStudents_WhenStudentISNotExist_ReturnsNotFound()
    {
        // Act
        var response = await client.GetAsync("/student/NonExistingId");

        // Assert
        response
            .StatusCode
            .Should()
            .Be(HttpStatusCode.NotFound);
    }

}
