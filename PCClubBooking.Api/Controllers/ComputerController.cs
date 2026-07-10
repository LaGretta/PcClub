using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCClubBooking.Application.DTOs;
using PCClubBooking.Application.Interfaces.Repository;
using PCClubBooking.Application.Interfaces.Service;

namespace PCClubBooking.Api.Controllers;

[ApiController]
[Route("api/computers")]
public class ComputerController : ControllerBase
{
   private readonly  IComputerService _service;
   public ComputerController(IComputerService service)
   {
       _service = service;
   }

   [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var computers = await _service.GetAllComputers();
        return Ok(computers);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var computer = await _service.GetComputerById(id);
        return Ok(computer);
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailable([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        var computers = await _service.GetAvailableComputers(start, end);
        return Ok(computers);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateComputerDto dto)
    {
        await _service.CreateComputer(dto);
        return Ok();
    }
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateComputerDto dto)
    {
        await _service.UpdateComputerById(dto, id);
        return Ok();
    }
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteComputerById(id);
        return NoContent();
    }
}