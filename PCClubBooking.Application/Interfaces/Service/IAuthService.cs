using PCClubBooking.Application.DTOs;

namespace PCClubBooking.Application.Interfaces.Service;

public interface IAuthService
{
    Task<AuthResponseDto> Register(RegisterDto registerDto);
    Task<AuthResponseDto> Login(LoginDto loginDto);
}