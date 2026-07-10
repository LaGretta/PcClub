using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCClubBooking.Application.DTOs;
using PCClubBooking.Application.Interfaces.Service;

namespace PCClubBooking.Api.Controllers;

[ApiController]
[Route("api")]
public class DeviceController : ControllerBase
{
    private readonly IDeviceService _deviceService;

    public DeviceController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }
    [HttpGet("computers/{computerId:int}/devices")]
    public async Task<IActionResult> GetByComputer(int computerId)
    {
        var devices = await _deviceService.GetDevicesByComputerId(computerId);
        return Ok(devices);
    }

    [HttpPost("computers/{computerId:int}/devices")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Add(int computerId, [FromBody] CreateDeviceDto dto)
    {
        await _deviceService.AddDeviceToPc(dto, computerId);
        return Ok();
    }

    [HttpPut("devices/{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDeviceDto dto)
    {
        await _deviceService.UpdateDeviceById(dto, id);
        return Ok();
    }

    [HttpDelete("devices/{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _deviceService.DeleteDeviceFromPc(id);
        return NoContent();
    }
}