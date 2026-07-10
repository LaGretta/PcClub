using Microsoft.EntityFrameworkCore;
using PCClubBooking.Application.Interfaces;
using PCClubBooking.Application.Interfaces.Repository;
using PCClubBooking.Domain.Entities;
using PCClubBooking.Infrastructure.Data;

namespace PCClubBooking.Infrastructure.Repositories;

public class DeviceRepository : IDeviceRepository
{
    private readonly AppDbContext _dbContext;
    public DeviceRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Device>> GetDevicesByComputerId(int computerId)
        => await _dbContext.Devices.Where(n => n.ComputerId == computerId).ToListAsync();
    
    public async Task<Device?> GetDeviceById(int id)
    {
        var find = await _dbContext.Devices.FirstOrDefaultAsync(n => n.Id == id);
        return find;
    }
    public async Task AddDevice(Device device)
    {
         await _dbContext.AddAsync(device);
    }
    public Task UpdateDevice(Device device)
    {
        _dbContext.Update(device);          
        return Task.CompletedTask;
    }
    public void DeleteDevice(Device device)
    {
         _dbContext.Remove(device);
    }
}