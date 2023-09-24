using MediatR;
using Microsoft.AspNetCore.Mvc;
using QimiaSchool1.Business.Implementations.Commands.Courses;
using QimiaSchool1.Business.Implementations.Commands.Courses.Dtos;
using QimiaSchool1.Business.Implementations.Queries.Course;
using QimiaSchool1.Business.Implementations.Queries.Course.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace QimiaSchool1.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]

public class CourseController : Controller
{
    private readonly IMediator _mediator;

    public CourseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> CreateCourse(
        [FromBody] CreateCourseDto course,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateCourseCommand(course),cancellationToken);

        return CreatedAtAction(
            nameof(GetCourse),
            new { id = response },
            course);
    }

    [HttpGet("{id}")]
    public Task<CourseDto> GetCourse(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        return _mediator.Send(
            new GetCourseQuery(id),
            cancellationToken);
    }

    [HttpGet]
    public Task<List<CourseDto>> GetCourses(CancellationToken cancellationToken)
    {
        return _mediator.Send(
            new GetCoursesQuery(),
            cancellationToken);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCourse(
        [FromRoute] int id,
        [FromBody] UpdateCourseDto course,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new UpdateCourseCommand(course, id),
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourse(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteCourseCommand(id),
            cancellationToken);

        return NoContent();
    }

}
