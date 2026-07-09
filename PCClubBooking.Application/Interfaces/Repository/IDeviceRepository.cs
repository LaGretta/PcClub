using PCClubBooking.Domain.Entities;

namespace PCClubBooking.Application.Interfaces.Repository;

public interface IDeviceRepository
{
    Task<List<Device>> GetDevicesByPcId(int computerId);
    Task AddDevicesForPcById(Device device);
    Task UpdateDevicesForPcById(Device device);
    void DeleteDevicesForPcById(Device device);
}