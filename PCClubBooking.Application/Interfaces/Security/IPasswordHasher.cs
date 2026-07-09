namespace PCClubBooking.Application.Interfaces.Security;

public interface IPasswordHasher
{
    Task<int> HashPasswordAsync(string password);
    bool Verify(string password, string hash);
}