using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using UKParliament.CodeTest.Services;
using UKParliament.CodeTest.Services.RequestResponseModels;

namespace UKParliament.CodeTest.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PeopleController : ControllerBase
{
    private readonly IPersonService _personService;

    public PeopleController(IPersonService personService)
    {
        _personService = personService;
    }

    [Route("{id:int}")]
    [HttpGet]
    public async Task<ActionResult<PersonResponse>> GetPerson(int id)
    {
        var result = await _personService.GetPerson(id);

        if (result.IsNotFound())
        {
            return NotFound();
        }

        return Ok(result.Value);
    }

    [Route("")]
    [HttpGet]
    public async Task<ActionResult<List<PersonResponse>>> GetPeople()
    {
        var people = await _personService.GetPeople();

        return Ok(people.Value);
    }

    [Route("")]
    [HttpPost]
    public async Task<ActionResult> Create(PersonRequest request)
    {
        var result = await _personService.CreatePerson(request);

        if (result.ValidationErrors.Any())
        {
            return BadRequest(result.ValidationErrors);
        }

        return CreatedAtAction(
               actionName: nameof(GetPerson),
               routeValues: new { id = result.Value.Id },
               value: result.Value);
    }

    [Route("{id:int}")]
    [HttpPut()]
    public async Task<ActionResult> Update(int id, PersonRequest request)
    {
        var result = await _personService.UpdatePerson(id, request);

        if (result.IsNotFound())
        {
            return NotFound();
        }

        if (result.ValidationErrors.Any())
        {
            return BadRequest(result.ValidationErrors);
        }

        return NoContent();
    }
}