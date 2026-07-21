using CAS.Contracts.Enums;
using CAS.DTOs;
using CAS.DTOs.Crop;
using CAS.Interfaces.Repositories;
using CAS.Interfaces.Services;
using CAS.Models.Entities;

namespace CAS.Implementation.Services
{
    public class CropService(ICropRepository cropRepository, ILogger<CropService> logger, IUnitOfWork unitOfWork) : ICropService
    {
        private readonly ICropRepository _cropRepository = cropRepository ?? throw new ArgumentNullException(nameof(cropRepository));
        private readonly ILogger<CropService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        public async Task<BaseResponse> CreateCropAsync(string imageUrl, CreateCropRequestModel model)
        {
            var cropExists = await _cropRepository.Any<Crop>(c => c.Name == model.Name);
            if (cropExists) return new BaseResponse
            {
                Message = "Crop with the same name already exists.",
                IsSuccess = false
            };

            var newCrop = new Crop
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = imageUrl,
                CropStatus = Status.Active,
                CreatedAt = DateTime.UtcNow,
            };

            await _cropRepository.Add(newCrop);
            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0 ? new BaseResponse
            {
                Message = "Crop created successfully.",
                IsSuccess = true
            }
            :
            new BaseResponse
            {
                Message = "Failed to create crop",
                IsSuccess = false
            };

        }

        public async Task<BaseResponse<IReadOnlyList<CropListResponseModel>>> GetAllCropsAsync()
        {
            var crops = await _cropRepository.GetAll<Crop>();

            if (!crops.Any() || crops is null) return new BaseResponse<IReadOnlyList<CropListResponseModel>>
            {
                Message = "No crop found",
                IsSuccess = false
            };
            
            var cropsData = crops.Select(c => new CropListResponseModel
            {
                Id = c.Id,
                Name = c.Name,
                ImageUrl = c.ImageUrl

            }).ToList();

            return new BaseResponse<IReadOnlyList<CropListResponseModel>>
            {
                Message = $"{crops.Count} Crops retrieved successfully",
                IsSuccess = true,
                Data = cropsData
            };

        }

        public async Task<BaseResponse<CropDto>> GetCropByIdAsync(Guid cropId)
        {
            var crop = await _cropRepository.Get<Crop>(c => c.Id == cropId);
            if (crop is null) return new BaseResponse<CropDto>
            {
                Message = "Crop cannot be found",
                IsSuccess = false
            };

            var cropData = new CropDto
            {
                Id = crop.Id,
                Name = crop.Name,
                Description = crop.Description,
                ImageUrl = crop.ImageUrl,
                CropStatus = crop.CropStatus,
                CreatedAt = crop.CreatedAt.ToLocalTime()
            };

            return new BaseResponse<CropDto>
            {
                Message = "Crop retrieved successfully",
                IsSuccess = true,
                Data = cropData
            };
        }
    }
}
