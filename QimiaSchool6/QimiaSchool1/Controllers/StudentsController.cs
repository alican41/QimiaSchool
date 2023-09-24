﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using QimiaSchool1.Business.Implementations.Commands.Students.Dtos;
using QimiaSchool1.Business.Implementations.Commands.Students;
using QimiaSchool1.Business.Implementations.Queries.Student.Dtos;
using QimiaSchool1.Business.Implementations.Queries.Student;
using Serilog;


namespace QimiaSchool1.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class StudentsController : Controller
{
    private readonly IMediator _mediator;
    private readonly Serilog.ILogger _studentLogger;
    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
        _studentLogger = Log.ForContext(
        "SourceContext",
        typeof(StudentsController).FullName);
        

    }

    


    [HttpPost]
    public async Task<ActionResult> CreateStudent(
        [FromBody] CreateStudentDto student,
        CancellationToken cancellationToken)
    {

        // Serilog with context
        _studentLogger.Information(
        "Create student request is accepted. Student:{@student}",
        student);

        var response = await _mediator.Send(new CreateStudentCommand(student), cancellationToken);

        return CreatedAtAction(
            nameof(GetStudent),
            new { Id = response },
            student);
    }

    [HttpGet("{id}")]
    public Task<StudentDto> GetStudent(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        return _mediator.Send(
            new GetStudentQuery(id),
            cancellationToken);
    }

    [HttpGet]
    public Task<List<StudentDto>> GetStudents(CancellationToken cancellationToken)
    {
        return _mediator.Send(
            new GetStudentsQuery(),
            cancellationToken);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateStudent(
        [FromRoute] int id,
        [FromBody] UpdateStudentDto student,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new UpdateStudentCommand(student, id),
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteStudent(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(
            new DeleteStudentCommand(id),
            cancellationToken);

        return NoContent();
    }
}
