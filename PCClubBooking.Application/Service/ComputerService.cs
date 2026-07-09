using AutoMapper;
using PCClubBooking.Application.DTOs;
using PCClubBooking.Application.Interfaces;
using PCClubBooking.Application.Interfaces.Repository;
using PCClubBooking.Application.Interfaces.Service;
using PCClubBooking.Domain.Entities;

namespace PCClubBooking.Application.Service;

public class ComputerService : IComputerService
{
    private readonly IMapper _mapper;
    private readonly IComputerRepository _computerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ComputerService(IMapper mapper
        , IComputerRepository computerRepository
        , IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _computerRepository = computerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ComputerResponseDto>> GetAllComputers()
    {
         var find = await _computerRepository.GetAllComputers();
         return _mapper.Map<List<ComputerResponseDto>>(find);
    }
    public async Task<ComputerResponseDto?> GetComputerById(int id)
    {
         var get = await _computerRepository.GetComputerById(id);
         if(get == null)
             throw new KeyNotFoundException("Computer not found");
         return _mapper.Map<ComputerResponseDto>(get);
    }
    public async Task<List<ComputerResponseDto>> GetAvailableComputers(DateTime start, DateTime end)
    {
         var computers = await _computerRepository.GetAvailableComputers(start, end); 
         return _mapper.Map<List<ComputerResponseDto>>(computers);
    }
    public async Task CreateComputer(CreateComputerDto createComputerDto)
    {
        var computer = _mapper.Map<Computer>(createComputerDto);
        await _computerRepository.CreateComputer(computer);
        await _unitOfWork.SaveChangesAsync();
    }
    public async Task UpdateComputerById(UpdateComputerDto updateComputerDto, int id)
    {
        var findpc = await _computerRepository.GetComputerById(id);
        if(findpc == null)
            throw new KeyNotFoundException("Computer not found");
        _mapper.Map(updateComputerDto, findpc);
        await _computerRepository.UpdateComputer(findpc);
        await _unitOfWork.SaveChangesAsync();
    }
    public async Task DeleteComputerById(int id)
    {
         var findpc = await _computerRepository.GetComputerById(id);
         if (findpc == null)
             throw new KeyNotFoundException("Computer not found");
         _computerRepository.DeleteComputer(findpc);
         await _unitOfWork.SaveChangesAsync();
    }
}