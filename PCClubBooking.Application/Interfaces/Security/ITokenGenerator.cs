using PCClubBooking.Domain.Entities;

namespace PCClubBooking.Application.Interfaces.Security;

public interface ITokenGenerator
{
    string GenerateToken(User user);
}