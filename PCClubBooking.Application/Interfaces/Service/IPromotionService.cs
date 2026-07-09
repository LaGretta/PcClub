using PCClubBooking.Application.DTOs;

namespace PCClubBooking.Application.Interfaces.Service;

public interface IPromotionService
{
    Task<List<PromotionResponseDto>> GetAllPromotions();
    Task<PromotionResponseDto?> GetPromotionById(int id);
    Task CreatePromotion(CreatePromotionDto createPromotionDto);
    Task UpdatePromotionById(UpdatePromotionDto updatePromotionDto , int id);
    Task DeletePromotionById(int id);
}