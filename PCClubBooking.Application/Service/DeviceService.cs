using AutoMapper;
using PCClubBooking.Application.DTOs;
using PCClubBooking.Application.Interfaces;
using PCClubBooking.Application.Interfaces.Repository;
using PCClubBooking.Application.Interfaces.Service;
using PCClubBooking.Domain.Entities;

namespace PCClubBooking.Application.Service;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IComputerRepository _computerRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeviceService(IDeviceRepository deviceRepository
        , IMapper mapper
        , IUnitOfWork unitOfWork
        , IComputerRepository computerRepository)
    {
        _deviceRepository = deviceRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _computerRepository = computerRepository;
    }
    public async Task<List<DeviceResponseDto>> GetDevicesByComputerId(int computerId)
    {
        var find = await _computerRepository.GetComputerById(computerId);
        if (find == null)
            throw new KeyNotFoundException("Computer not found");
        return _mapper.Map<List<DeviceResponseDto>>(find.Devices);
    }
    public async Task AddDeviceToPc(CreateDeviceDto createDeviceDto, int computerId)
    {
        var findPc = await _computerRepository.GetComputerById(computerId);
        if(findPc == null)
            throw new KeyNotFoundException("Computer not found");
        var add = _mapper.Map<Device>(createDeviceDto);
        findPc.Devices.Add(add);
        await _computerRepository.UpdateComputer(findPc);
        await _unitOfWork.SaveChangesAsync();
    }
    public async Task UpdateDeviceById(UpdateDeviceDto updateDeviceDto, int id)
    {
        var device = await _deviceRepository.GetDeviceById(id); 
        if (device == null)
            throw new KeyNotFoundException("Device not found");
        _mapper.Map(updateDeviceDto, device);                     
        await _deviceRepository.UpdateDevice(device);
        await _unitOfWork.SaveChangesAsync();
    }
    public async Task DeleteDeviceFromPc(int id)
    {
        var device = await _deviceRepository.GetDeviceById(id);   
        if (device == null)                                       
            throw new KeyNotFoundException("Device not found");
        _deviceRepository.DeleteDevice(device);                
        await _unitOfWork.SaveChangesAsync();
    }
}