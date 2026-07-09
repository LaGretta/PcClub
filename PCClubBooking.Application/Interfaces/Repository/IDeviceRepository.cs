using PCClubBooking.Domain.Entities;

namespace PCClubBooking.Application.Interfaces.Repository;

public interface IDeviceRepository
{
    Task<List<Device>> GetDevicesByComputerId(int computerId);   
    Task<Device?> GetDeviceById(int id);                          
    Task AddDevice(Device device);
    Task UpdateDevice(Device device);
    void DeleteDevice(Device device);
}