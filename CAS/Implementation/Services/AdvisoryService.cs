using CAS.AdvisoryHandler;
using CAS.DTOs;
using CAS.DTOs.Advisory;
using CAS.Interfaces.Repositories;
using CAS.Interfaces.Services;
using CAS.Models.Entities;

namespace CAS.Implementation.Services
{
    public class AdvisoryService : IAdvisoryService
    {
        private readonly ILogger<AdvisoryService> _logger;
        private readonly IAdvisoryRepository _advisoryRepository;
        private readonly ISeasonRepository _seasonRepository;
        private readonly ICropRepository _cropRepository;
        private readonly ISoilTypeRepository _soilTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AdvisoryService(ILogger<AdvisoryService> logger, IAdvisoryRepository advisoryRepository, 
            ISeasonRepository seasonRepository, 
            ICropRepository cropRepository, 
            ISoilTypeRepository soilTypeRepository, 
            IUnitOfWork unitOfWork)
        {
            _advisoryRepository = advisoryRepository ?? throw new ArgumentNullException(nameof(advisoryRepository));
            _seasonRepository = seasonRepository;
            _cropRepository = cropRepository;
            _soilTypeRepository = soilTypeRepository;
            _unitOfWork = unitOfWork;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<BaseResponse> CreateAdvisoryAsync(List<CreateAdvisoryRequestModel> request)
        {
            int advisoriesCount = 0;
            int maxAdvisories = 50;
            int successfulAdvisories = 0;

            var seasonIds = request.Select(r => r.SeasonId).ToList();
            var seasons = await _seasonRepository.GetByIds<Season>(seasonIds);

            if(seasonIds.Count != seasons.Count)
            {
                return new BaseResponse
                {
                    IsSuccess = false,
                    Message = "One or more seasons not found"
                };
            }

            var cropIds = request.Select(r => r.CropId).ToList();
            var crops = await _cropRepository.GetByIds<Crop>(cropIds);

            if(cropIds.Count != crops.Count)
            {
                return new BaseResponse
                {
                    IsSuccess = false,
                    Message = "One or more crops not found"
                };
            }

            var soilTypeIds = request.Select(r => r.SoilTypeId).ToList();
            var soilTypes = await _soilTypeRepository.GetByIds<SoilType>(soilTypeIds);

            if(soilTypeIds.Count != soilTypes.Count)
            {
                return new BaseResponse
                {
                    IsSuccess = false,
                    Message = "One or more soil types not found"
                };
            }

            var requests = request.Select(r => new { r.CropId, r.SoilTypeId, r.SeasonId, r.Title }).ToList();

            var duplicateRequests = requests.GroupBy(r => new { r.CropId, r.SoilTypeId, r.SeasonId, r.Title })
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
              (a => a.Title == item.Title
              && a.CropId == item.CropId
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
    }
}
