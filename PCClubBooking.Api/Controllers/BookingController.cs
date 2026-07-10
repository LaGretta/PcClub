using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCClubBooking.Application.DTOs;
using PCClubBooking.Application.Interfaces.Service;

namespace PCClubBooking.Api.Controllers;

[ApiController]
[Route("api/bookings")]
[Authorize] 
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }
    private int GetUserId()
        => int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingDto dto)
    {
        var userId = GetUserId();
        var booking = await _bookingService.CreateBooking(dto, userId);
        return Ok(booking);
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetMy()
    {
        var userId = GetUserId();
        var bookings = await _bookingService.GetAllMyBookings(userId);
        return Ok(bookings);
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var booking = await _bookingService.GetBookingById(id);
        return Ok(booking);
    }

    [HttpPost("{id:int}/cancel")]
    public async Task<IActionResult> Cancel(int id)
    {
        var userId = GetUserId();
        var booking = await _bookingService.CancelBooking(id, userId);
        return Ok(booking);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _bookingService.GetAllBookings(page, pageSize);
        return Ok(result);
    }
}