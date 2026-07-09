using AutoMapper;
using PCClubBooking.Application.DTOs;
using PCClubBooking.Application.Interfaces;
using PCClubBooking.Application.Interfaces.Repository;
using PCClubBooking.Application.Interfaces.Service;
using PCClubBooking.Domain.Entities;

namespace PCClubBooking.Application.Service;

public class PromotionService : IPromotionService
{
    private readonly IPromotionRepository _promotionRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PromotionService(IPromotionRepository promotionRepository
        , IMapper mapper
        , IUnitOfWork unitOfWork)
    {
        _promotionRepository = promotionRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<PromotionResponseDto>> GetAllPromotions()
    {
        var all = await _promotionRepository.GetAllPromotionsAsync();
        return _mapper.Map<List<PromotionResponseDto>>(all);
    }
    public async Task<PromotionResponseDto?> GetPromotionById(int id)
    {
        var find = await  _promotionRepository.GetPromotionByIdAsync(id);
        if (find == null)
            throw new KeyNotFoundException($"Promotion with id {id} not found");
        return _mapper.Map<PromotionResponseDto>(find);
    }
    public async Task CreatePromotion(CreatePromotionDto createPromotionDto)
    {
        var promotion = _mapper.Map<Promotion>(createPromotionDto);
        await _promotionRepository.CreatePromotionAsync(promotion);
        await _unitOfWork.SaveChangesAsync();
    }
    public async Task UpdatePromotionById(UpdatePromotionDto updatePromotionDto, int id)
    {
        var find = await _promotionRepository.GetPromotionByIdAsync(id);
        if (find == null)
            throw new KeyNotFoundException($"Promotion with id {id} not found");
        _mapper.Map(updatePromotionDto, find);
        await _promotionRepository.UpdatePromotionAsync(find);
        await _unitOfWork.SaveChangesAsync();
    }
    public async Task DeletePromotionById(int id)
    {
        var find = await _promotionRepository.GetPromotionByIdAsync(id);
        if (find == null)
            throw new KeyNotFoundException($"Promotion with id {id} not found");
        _promotionRepository.DeletePromotionById(find);
        await _unitOfWork.SaveChangesAsync(); 
    }
}