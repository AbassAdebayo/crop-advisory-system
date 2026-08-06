using CAS.AdvisoryHandler;
using CAS.DTOs;
using CAS.DTOs.Advisory;
using CAS.Interfaces.Repositories;
using CAS.Interfaces.Services;
using CAS.Models.Entities;
using System.Runtime.Intrinsics.Arm;

namespace CAS.Implementation.Services
{
    public class AdvisoryService : IAdvisoryService
    {
        private readonly ILogger<AdvisoryService> _logger;
        private readonly IAdvisoryRepository _advisoryRepository;
        private readonly ISeasonRepository _seasonRepository;
        private readonly ICropRepository _cropRepository;
        private readonly ISoilTypeRepository _soilTypeRepository;
        private readonly ISaveGuideRepository _saveGuideRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AdvisoryService(ILogger<AdvisoryService> logger, IAdvisoryRepository advisoryRepository, 
            ISeasonRepository seasonRepository, 
            ICropRepository cropRepository, 
            ISoilTypeRepository soilTypeRepository, 
            ISaveGuideRepository saveGuideRepository,
            IUnitOfWork unitOfWork)
        {
            _advisoryRepository = advisoryRepository ?? throw new ArgumentNullException(nameof(advisoryRepository));
            _seasonRepository = seasonRepository;
            _cropRepository = cropRepository;
            _soilTypeRepository = soilTypeRepository;
            _unitOfWork = unitOfWork;
            _saveGuideRepository = saveGuideRepository;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<BaseResponse> CreateAdvisoryAsync(List<CreateAdvisoryRequestModel> request)
        {
            int advisoriesCount = 0;
            int maxAdvisories = 50;
            int successfulAdvisories = 0;

            var seasonIds = request.Select(r => r.SeasonId).Distinct().ToList();
            var seasons = await _seasonRepository.GetByIds<Season>(seasonIds);

            if(seasonIds.Count != seasons.Count)
            {
                return new BaseResponse
                {
                    IsSuccess = false,
                    Message = "One or more seasons not found"
                };
            }

            var cropIds = request.Select(r => r.CropId).Distinct().ToList();
            var crops = await _cropRepository.GetByIds<Crop>(cropIds);

            if(cropIds.Count != crops.Count)
            {
                return new BaseResponse
                {
                    IsSuccess = false,
                    Message = "One or more crops not found"
                };
            }

            var soilTypeIds = request.Select(r => r.SoilTypeId).Distinct().ToList();
            var soilTypes = await _soilTypeRepository.GetByIds<SoilType>(soilTypeIds);

            if(soilTypeIds.Count != soilTypes.Count)
            {
                return new BaseResponse
                {
                    IsSuccess = false,
                    Message = "One or more soil types not found"
                };
            }

            var requests = request.Select(r => new { r.CropId, r.SoilTypeId, r.SeasonId }).ToList();

            var duplicateRequests = requests.GroupBy(r => new { r.CropId, r.SoilTypeId, r.SeasonId })
                                            .Where(g => g.Count() > 1)
                                            .Select(g => g.Key)
                                            .ToList();

            if (duplicateRequests.Any())
            {
                return new BaseResponse
                {
                    IsSuccess = false,
                    Message = "Duplicate advisories found in the request for the same crop, soil type, and season."
                };
            }


            foreach ( var item in request)
            {
              if(advisoriesCount > maxAdvisories)
               {
                    return new BaseResponse
                    {
                        Message = "You cannot create more than 50 advisories at a go",
                        IsSuccess = false,
                    };
               }

              var advisoryExists = await _advisoryRepository.Any<Advisory>
              (a => a.CropId == item.CropId
              && a.SoilTypeId == item.SoilTypeId
              && a.SeasonId == item.SeasonId);


                if (advisoryExists)
                {
                    return new BaseResponse
                    {
                        IsSuccess = false,
                        Message = "Advisory already exists for the given crop, soil type, and season."
                    };
                }

                var advisory = new Advisory
                {
                    CropId = item.CropId,
                    SoilTypeId = item.SoilTypeId,
                    SeasonId = item.SeasonId,
                    Title = item.Title,
                    Location = item.Location
                };

                var crop = await _cropRepository.Get<Crop>(c => c.Id ==  item.CropId);
                var season = await _seasonRepository.Get<Season>(s => s.Id == item.SeasonId);
                var soilType = await _soilTypeRepository.Get<SoilType>(s => s.Id == item.SoilTypeId);

                if (crop is null || season is null || soilType is null) continue;

                advisory.WateringAdvice = AdvisoryGenerator.GetWateringAdvice(crop.Name, season.Name);
                advisory.FertilizerAdvice = AdvisoryGenerator.GetFertilizerAdvice(crop.Name);
                advisory.HarvestingTips = AdvisoryGenerator.GetHarvestingTips(crop.Name);
                advisory.PestControlAdvice = AdvisoryGenerator.GetPestControlAdvice(crop.Name);

               var advisoryStatus =  await _advisoryRepository.Add<Advisory>(advisory);

               if(advisoryStatus is not null) successfulAdvisories++;

                advisoriesCount++;

            }


            var result = await _unitOfWork.SaveChangesAsync();

            return result > 0 ? new BaseResponse
            {
                Message = $"{successfulAdvisories} advisory records saved successfully to database",
                IsSuccess = true
            } :
            new BaseResponse
            {
                Message = "Failed to save advisories to database",
                IsSuccess = false
            };

        }


        public async Task<BaseResponse<PagedResponse<AdvisoryCardResponseModel>>> SearchAsync(SearchAdvisoryRequestModel request)
        {
            var result = await _advisoryRepository.SearchAsync(
           request.Keyword,
           request.CropId,
           request.SeasonId,
           request.SoilTypeId,
           request.Page,
           request.PageSize);

            var response = new PagedResponse<AdvisoryCardResponseModel>
            {
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize,
                TotalPages = result.TotalPages,
                TotalRecords = result.TotalRecords,

                Data = result.Items.Select(x => new AdvisoryCardResponseModel
                {
                    Id = x.Id,

                    Crop = x.Crop.Name,

                    Season = x.Season.Name,

                    SoilType = x.SoilType.Name,

                    Location = x.Location!,

                    Title = x.Title,
                }).ToList()
            };

            return new BaseResponse<PagedResponse<AdvisoryCardResponseModel>>
            {
                IsSuccess = true,
                Message = "Advisories retrieved successfully.",
                Data = response
            };

        }

        public async Task<BaseResponse<IReadOnlyList<AdvisoryCardResponseModel>>> GetFavouriteAdvisoriesAsync(Guid userId)
        {
            var favourites = await _saveGuideRepository.GetFavourites(userId);
            if (favourites is null || !favourites.Any())
                return new BaseResponse<IReadOnlyList<AdvisoryCardResponseModel>>
                {
                    IsSuccess = false,
                    Message = "No favourites found"
                };

                var response = favourites.Select(x => new AdvisoryCardResponseModel
                {
                    Id = x.Id,

                    Crop = x.Advisory.Crop.Name,

                    Season = x.Advisory.Season.Name,

                    SoilType = x.Advisory.SoilType.Name,

                    Location = x.Advisory.Location!,

                    Title = x.Advisory.Title,
                }).ToList();

            return new BaseResponse<IReadOnlyList<AdvisoryCardResponseModel>>
            {
                Message = "Favourite advisories retrieved successfully",
                IsSuccess = true,
                Data = response
            };
            
        }

        public async Task<BaseResponse<AdvisoryDetailsResponseModel>> GetFavouriteAdvisoryDetails(Guid advisoryId, Guid userId)
        {
            var isFavourite = await _saveGuideRepository.Any<SaveGuide>(s => s.AdvisoryId == advisoryId && s.UserId == userId);

            var advisory = await _advisoryRepository.GetByIdAsync(advisoryId);

            if (advisory == null)
            {
                return new BaseResponse<AdvisoryDetailsResponseModel>
                {
                    IsSuccess = false,
                    Message = "Advisory not found."
                };
            }

            return new BaseResponse<AdvisoryDetailsResponseModel>
            {
                IsSuccess = true,
                Message = "Success",

                Data = new AdvisoryDetailsResponseModel
                {
                    Id = advisory.Id,

                    Crop = advisory.Crop.Name,

                    Season = advisory.Season.Name,

                    SoilType = advisory.SoilType.Name,

                    Location = advisory.Location!,

                    Title = advisory.Title,

                    WateringAdvice = advisory.WateringAdvice,

                    FertilizerAdvice = advisory.FertilizerAdvice,

                    PestControlAdvice = advisory.PestControlAdvice,

                    HarvestingTips = advisory.HarvestingTips,

                    IsFavourite = isFavourite,

                    CreatedAt = advisory.CreatedAt
                }
            };
        }

        public async Task<BaseResponse> ToggleFavouriteAsync(Guid farmerId, Guid advisoryId)
        {
            var farmerFavourite = await _saveGuideRepository.Get<SaveGuide>(s => s.UserId == farmerId && s.AdvisoryId == advisoryId);


            if (farmerFavourite is not null)
            {
                _saveGuideRepository.Delete(farmerFavourite);
                var removeResponse = await _unitOfWork.SaveChangesAsync();

                return removeResponse > 0 ? new BaseResponse { IsSuccess = true, Message = "Favourite advisory removed successfully." } :
                    new BaseResponse { IsSuccess = false, Message = "Failed to remove favourite advisory." };
            }

            var addFavourite = new SaveGuide
            {
                UserId = farmerId,
                AdvisoryId = advisoryId,
                CreatedAt = DateTime.UtcNow
            };

            await _saveGuideRepository.Add<SaveGuide>(addFavourite);

           var addResponse =  await _unitOfWork.SaveChangesAsync();
            return addResponse > 0 ? new BaseResponse { IsSuccess = true, Message = "Favourite addedd successfully" } :
                 new BaseResponse { IsSuccess = false, Message = "Failed to add favourite advisory" };


            
        }
    }
}
