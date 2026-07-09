using PCClubBooking.Domain.Entities;

namespace PCClubBooking.Application.Interfaces.Repository;

public interface IComputerRepository
{
    Task<List<Computer>> GetAllComputers();
    Task<Computer?> GetComputerById(int id);
    Task<List<Computer>> GetAvailableComputers(DateTime start, DateTime end);
    Task CreateComputer(Computer computer);
    Task UpdateComputer(Computer computer);
    void DeleteComputer(Computer computer);
}