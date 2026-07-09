using PCClubBooking.Domain.Entities;

namespace PCClubBooking.Application.Interfaces.Repository;

public interface IBookingRepository
{
    Task CreateBooking(Booking booking);
    Task<List<Booking>> GetMyBooking(int userId);
    Task<Booking?> GetMyBookingById(int userId ,int bookingId);
    Task CancelBooking(Booking booking);
    Task<bool> HasOverlappingBookingAsync(int computerId, DateTime start, DateTime end);
    Task<(List<Booking> Items, int TotalCount)> GetAllPagedAsync(int page, int pageSize);
}