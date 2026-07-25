using CAS.Contracts.Enums;
using CAS.DTOs;
using CAS.DTOs.SoilType;
using CAS.Implementation.Repositories;
using CAS.Interfaces.Repositories;
using CAS.Interfaces.Services;
using CAS.Models.Entities;

namespace CAS.Implementation.Services
{
    public class SoilTypeService(ISoilTypeRepository soilTypeRepository, IUnitOfWork unitOfWork) : ISoilTypeService
    {
        private readonly ISoilTypeRepository _soilTypeRepository = soilTypeRepository ?? throw new ArgumentNullException(nameof(soilTypeRepository));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

        public async Task<BaseResponse> ActivateSoilTypeStatusAsync(Guid id)
        {
            var soilType = await _soilTypeRepository.GetSoilTypeByIdForAdmin(id);
            if (soilType is null) return new BaseResponse { Message = $"Soil type with Id {id} cannot be found", IsSuccess = false };

            var newSoilTypeStatus = Status.Active;

            soilType.ActivateSoilTypeStatus(newSoilTypeStatus);

            await _soilTypeRepository.Update(soilType);

            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0 ? new BaseResponse { Message = "Soil type activated successfully", IsSuccess = true } :
                new BaseResponse { Message = "Error while activating soil type", IsSuccess = false };
        }

        public async Task<BaseResponse> DeactivateSoilTypeStatusAsync(Guid id)
        {
            var soilType = await _soilTypeRepository.GetSoilTypeByIdForAdmin(id);
            if (soilType is null) return new BaseResponse { Message = $"Soil type with Id {id} cannot be found", IsSuccess = false };

            var newSoilTypeStatus = Status.Inactive;

            soilType.DeactivateCropStatus(newSoilTypeStatus);

            await _soilTypeRepository.Update(soilType);

            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0 ? new BaseResponse { Message = "Soil type deactivated successfully", IsSuccess = true } :
                new BaseResponse { Message = "Error while deactivating soil type", IsSuccess = false };
        }

        public async Task<BaseResponse<IReadOnlyList<SoilTypeListResponseModel>>> GetAllSoilTypesForAdminAsync()
        {
            var soilTypes = await _soilTypeRepository.GetAllSoilTypesForAdmin();
            if (soilTypes is null || !soilTypes.Any())
                return  new BaseResponse<IReadOnlyList<SoilTypeListResponseModel>> { Message = "No soiltype found", IsSuccess = false };

            var soilTypesCount = soilTypes.Count();

            var soilTypeResponse = soilTypes.Select(s => new SoilTypeListResponseModel
            {
                Id = s.Id,
                Name = s.Name,
                SoilTypeStatus = s.SoilTypeStatus
            }).ToList();


            return new BaseResponse<IReadOnlyList<SoilTypeListResponseModel>> 
            { Message = $"{soilTypesCount} retrieved successfully", IsSuccess = true, Data = soilTypeResponse };

        }

        public async Task<BaseResponse<IReadOnlyList<SoilTypeListResponseModel>>> GetAllSoilTypesForFarmerAsync()
        {
            var soilTypes = await _soilTypeRepository.GetAll<SoilType>();
            if (soilTypes is null || !soilTypes.Any())
                return new BaseResponse<IReadOnlyList<SoilTypeListResponseModel>> { Message = "No soiltype found", IsSuccess = false };

            var soilTypesCount = soilTypes.Count();

            var soilTypeResponse = soilTypes.Select(s => new SoilTypeListResponseModel
            {
                Id = s.Id,
                Name = s.Name,
                SoilTypeStatus = s.SoilTypeStatus
            }).ToList();


            return new BaseResponse<IReadOnlyList<SoilTypeListResponseModel>>
            { Message = $"{soilTypesCount} retrieved successfully", IsSuccess = true, Data = soilTypeResponse };
        }

        public async Task<BaseResponse<SoilTypeDto>> GetSoilTypeDetailsForAdminAsync(Guid id)
        {
            var soilType = await _soilTypeRepository.GetSoilTypeByIdForAdmin(id);
            if (soilType is null) return new BaseResponse<SoilTypeDto> { Message = $"Soiltype with ID {id} couldn't be retrieved", IsSuccess = false };

            var soilTypeDetailsResponse = new SoilTypeDto
            {
                Id = soilType.Id,
                Name = soilType.Name,
                Description = soilType.Description,
                SoilTypeStatus = soilType.SoilTypeStatus,
                CreatedAt = DateTime.Now,

            };

            return new BaseResponse<SoilTypeDto> { Message = "Soiltype retrieved successfully", IsSuccess = true, Data = soilTypeDetailsResponse };

        }

        public async Task<BaseResponse<SoilTypeDto>> GetSoilTypeDetailsForFarmerAsync(Guid id)
        {
            var soilType = await _soilTypeRepository.Get<SoilType>(s => s.Id == id);
            if (soilType is null) return new BaseResponse<SoilTypeDto> { Message = $"Soiltype with ID {id} couldn't be retrieved", IsSuccess = false };

            var soilTypeDetailsResponse = new SoilTypeDto
            {
                Id = soilType.Id,
                Name = soilType.Name,
                Description = soilType.Description,
                SoilTypeStatus = soilType.SoilTypeStatus,
                CreatedAt = DateTime.Now,

            };

            return new BaseResponse<SoilTypeDto> { Message = "Soiltype retrieved successfully", IsSuccess = true, Data = soilTypeDetailsResponse };
        }
    }
}
