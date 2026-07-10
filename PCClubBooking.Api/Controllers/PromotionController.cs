using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PCClubBooking.Application.DTOs;
using PCClubBooking.Application.Interfaces.Service;

namespace PCClubBooking.Api.Controllers;

[ApiController]
[Route("api/promotions")]
public class PromotionController : ControllerBase
{
    private readonly IPromotionService _promotionService;
    public PromotionController(IPromotionService promotionService)
    {
        _promotionService = promotionService;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllPromotions()
    {
        return Ok(await _promotionService.GetAllPromotions());
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var promotion = await _promotionService.GetPromotionById(id);
        return Ok(promotion);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreatePromotionDto dto)
    {
        await _promotionService.CreatePromotion(dto);
        return Ok();
    }
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePromotionDto dto)
    {
        await _promotionService.UpdatePromotionById(dto, id);
        return Ok();
    }
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _promotionService.DeletePromotionById(id);
        return NoContent();
    }
}