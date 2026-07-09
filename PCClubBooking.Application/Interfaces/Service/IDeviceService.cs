using PCClubBooking.Application.DTOs;

namespace PCClubBooking.Application.Interfaces.Service;

public interface IDeviceService
{
    Task<List<DeviceResponseDto>> GetDevicesByComputerId(int computerId);
    Task AddDeviceToPc(CreateDeviceDto createDeviceDto, int computerId);
    Task UpdateDeviceById(UpdateDeviceDto updateDeviceDto , int id);
    Task DeleteDeviceFromPc(int id);
}