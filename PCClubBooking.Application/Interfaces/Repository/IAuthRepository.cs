using PCClubBooking.Domain.Entities;

namespace PCClubBooking.Application.Interfaces.Repository;

public interface IAuthRepository
{
    Task<User?> GetUserByEmail(string email);
    Task<bool> ExistsUserByEmail(string email);
    Task AddUser(User user);
}