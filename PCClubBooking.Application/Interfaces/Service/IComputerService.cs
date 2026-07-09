using PCClubBooking.Application.DTOs;

namespace PCClubBooking.Application.Interfaces.Service;

public interface IComputerService
{
    Task<List<ComputerResponseDto>> GetAllComputers();
    Task<ComputerResponseDto?> GetComputerById(int id);
    Task<List<ComputerResponseDto>> GetAvailableComputers(DateTime start, DateTime end);
    Task CreateComputer(CreateComputerDto createComputerDto);
    Task UpdateComputerById(UpdateComputerDto updateComputerDto , int id);
    Task DeleteComputerById(int id);
}