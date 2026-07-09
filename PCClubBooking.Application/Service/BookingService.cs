using AutoMapper;
using PCClubBooking.Application.DTOs;
using PCClubBooking.Application.Interfaces;
using PCClubBooking.Application.Interfaces.Repository;
using PCClubBooking.Application.Interfaces.Service;
using PCClubBooking.Domain.Entities;
using PCClubBooking.Domain.Enums;

namespace PCClubBooking.Application.Service;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IComputerRepository _computerRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public BookingService(IBookingRepository bookingRepository
        , IComputerRepository computerRepository
        , IMapper mapper
        , IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _computerRepository = computerRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseBookingDto> CreateBooking(CreateBookingDto createBookingDto, int userId)
    {
        var findpc = await _computerRepository.GetComputerById(createBookingDto.ComputerId);
        if (findpc == null)
            throw new KeyNotFoundException("Computer not found");
        if (!findpc.IsWorking)
            throw new InvalidOperationException("Computer is not working");
        if (createBookingDto.EndTime <= createBookingDto.StartTime)
            throw new ArgumentException("EndTime must be after StartTime");
        if (await _bookingRepository.HasOverlappingBookingAsync(createBookingDto.ComputerId, createBookingDto.StartTime, createBookingDto.EndTime))
            throw new InvalidOperationException("Booking already exists");

        var hours = (decimal)(createBookingDto.EndTime - createBookingDto.StartTime).TotalHours;
        var totalPrice = hours * findpc.PricePerHour;

        var booking = _mapper.Map<Booking>(createBookingDto);
        booking.UserId = userId;                   
        booking.TotalPrice = totalPrice;         
        booking.Status = BookingStatus.Active;    
        booking.CreatedAt = DateTime.UtcNow;

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _bookingRepository.CreateBooking(booking);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
        return _mapper.Map<ResponseBookingDto>(booking);
    }
    public async Task<List<ResponseBookingDto>> GetAllMyBookings(int userId)
    {
        var bookings = await _bookingRepository.GetMyBooking(userId);
            return _mapper.Map<List<ResponseBookingDto>>(bookings);
    }
    public async Task<ResponseBookingDto> GetBookingById(int bookingId)
    {
        var find = await _bookingRepository.GetBookingById(bookingId);
        return _mapper.Map<ResponseBookingDto>(find);
    }

    public async Task<ResponseBookingDto> CancelBooking(int bookingId, int userId)
    {
        var find = await _bookingRepository.GetMyBookingById(userId , bookingId);
        if(find == null)
            throw new KeyNotFoundException("Booking not found");
        if (find.Status != BookingStatus.Active)
            throw new InvalidOperationException("You cannot cancel booking");
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            find.Status = BookingStatus.Cancelled;
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
        return _mapper.Map<ResponseBookingDto>(find);
    }

    public async Task<PagedResponse<ResponseBookingDto>> GetAllBookings(int page, int pageSize)
    {
        var result = await _bookingRepository.GetAllPagedAsync(page, pageSize);
        var items = _mapper.Map<List<ResponseBookingDto>>(result.Items);

        return new PagedResponse<ResponseBookingDto>()
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount =  result.TotalCount
        };
    }
}