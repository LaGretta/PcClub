using Microsoft.EntityFrameworkCore;
using PCClubBooking.Application.Interfaces.Repository;
using PCClubBooking.Domain.Entities;
using PCClubBooking.Domain.Enums;
using PCClubBooking.Infrastructure.Data;

namespace PCClubBooking.Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _dbContext;
    public BookingRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CreateBooking(Booking booking)
    {
       await _dbContext.Bookings.AddAsync(booking);
    }

    public async Task<List<Booking>> GetMyBooking(int userId)
    {
        return await _dbContext.Bookings.Where(b => b.UserId == userId).ToListAsync();
    }

    public async Task<Booking?> GetBookingById(int bookingId)
    {
         var booking = await _dbContext.Bookings.FirstOrDefaultAsync(b => b.Id == bookingId);
         return booking;
    }

    public async Task<Booking?> GetMyBookingById(int userId, int bookingId)
    {
         var  booking = await _dbContext.Bookings.FirstOrDefaultAsync(b => b.Id == bookingId && b.UserId == userId);
         return booking;
    }

    public async Task<bool> HasOverlappingBookingAsync(int computerId, DateTime start, DateTime end)
    {
        var find = await _dbContext.Bookings
            .AnyAsync(b => b.ComputerId == computerId
                           && b.Status == BookingStatus.Active
                           && b.StartTime < end
                           && b.EndTime > start);
        return find;
    }

    public async Task<(List<Booking> Items, int TotalCount)> GetAllPagedAsync(int page, int pageSize)
    {
        var get =  _dbContext.Bookings.AsNoTracking();

        var countAsync = await get.CountAsync();

        var paged = await get
            .OrderByDescending(n => n.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (paged, countAsync);
    }
}