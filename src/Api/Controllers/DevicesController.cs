using Application.Devices.Commands;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DevicesController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddDevice command)
    {
        var response = await mediator.Send(command);
        return CreatedAtAction("GetById", new { id = response.Id }, response);
    }

    [HttpPut("{id:Guid}", Name = "GetById")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateDevice command)
    {
        command.Id = id;
        var response = await mediator.Send(command);
        return Accepted(response);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var command = new GetDeviceById(id);
        var response = await mediator.Send(command);
        return Ok(response);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var command = new DeleteDevice(id);
        var response = await mediator.Send(command);
        return Ok(response);
    }

    [HttpPatch("{id:Guid}")]
    public async Task<IActionResult> PatchUpdateAsync([FromRoute] Guid id, [FromBody] JsonPatchDocument jsonPatchDocument)
    {
        var response = await mediator.Send(new PatchDevice(id, jsonPatchDocument));
        return Ok(response);
    }
}