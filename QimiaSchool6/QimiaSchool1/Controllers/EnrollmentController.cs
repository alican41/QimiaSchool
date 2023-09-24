using MediatR;
using Microsoft.AspNetCore.Mvc;
using QimiaSchool1.Business.Implementations.Commands.Enrollments;
using QimiaSchool1.Business.Implementations.Commands.Enrollments.Dtos;
using QimiaSchool1.Business.Implementations.Queries.Enrollment;
using QimiaSchool1.Business.Implementations.Queries.Enrollment.Dtos;
using Microsoft.AspNetCore.Authorization;


namespace QimiaSchool1.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class EnrollmentController : Controller
{
    private readonly IMediator _mediator;

    public EnrollmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult> CreateEnrollment(
        [FromBody] CreateEnrollmentDto enrollment,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new CreateEnrollmentCommand(enrollment), cancellationToken);

        return CreatedAtAction(
            nameof(GetEnrollment),
            new { id = response },
            enrollment);
    }
    [HttpGet("{id}")]
    public Task<EnrollmentDto> GetEnrollment(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        return _mediator.Send(
            new GetEnrollmentQuery(id),
            cancellationToken);
    }

    [HttpGet]
    public Task<List<EnrollmentDto>> GetEnrollments(CancellationToken cancellationToken)
    {
        return _mediator.Send(
            new GetEnrollmentsQuery(),
            cancellationToken);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateEnrollment(
        [FromRoute] int id,
        [FromBody] UpdateEnrollmentDto enrollment,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new UpdateEnrollmentCommand(enrollment, id),
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEnrollment(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteEnrollmentCommand(id),
            cancellationToken);

        return NoContent();
    }
}
