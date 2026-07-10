using Microsoft.EntityFrameworkCore;
using PCClubBooking.Application.Interfaces.Repository;
using PCClubBooking.Domain.Entities;
using PCClubBooking.Infrastructure.Data;

namespace PCClubBooking.Infrastructure.Repositories;

public class PromotionRepository : IPromotionRepository
{
    private readonly AppDbContext _dbContext;
    public PromotionRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Promotion>> GetAllPromotionsAsync()
        => await _dbContext.Promotions.ToListAsync();
    
    public async Task<Promotion?> GetPromotionByIdAsync(int id)
    {
         var find = await  _dbContext.Promotions.FirstOrDefaultAsync(p => p.Id == id);
         return find;
    }
    public async Task CreatePromotionAsync(Promotion promotion)
    {
         await _dbContext.Promotions.AddAsync(promotion);
    }
    public Task UpdatePromotionAsync(Promotion promotion)
    {
        _dbContext.Update(promotion);
        return Task.CompletedTask;
    }
    public void DeletePromotionById(Promotion promotion)
    {
        _dbContext.Remove(promotion);
    }
}