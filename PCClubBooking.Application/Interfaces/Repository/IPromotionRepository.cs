using PCClubBooking.Domain.Entities;

namespace PCClubBooking.Application.Interfaces.Repository;

public interface IPromotionRepository
{
    Task<List<Promotion>> GetAllPromotionsAsync();
    Task<Promotion?> GetPromotionByIdAsync(int id);
    Task CreatePromotionAsync(Promotion promotion);
    Task UpdatePromotionAsync(Promotion promotion);
    void DeletePromotionById(Promotion promotion);
}