using PCClubBooking.Application.DTOs;

namespace PCClubBooking.Application.Interfaces.Service;

public interface IBookingService
{
    Task<ResponseBookingDto> CreateBooking(CreateBookingDto createBookingDto, int userId);
    Task<List<ResponseBookingDto>> GetAllMyBookings(int userId);
    Task<ResponseBookingDto> GetBookingById(int bookingId);
    Task<ResponseBookingDto> CancelBooking(int bookingId , int userId);
    Task<PagedResponse<ResponseBookingDto>> GetAllBookings(int page, int pageSize);
}