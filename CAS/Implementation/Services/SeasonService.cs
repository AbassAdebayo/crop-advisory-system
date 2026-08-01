using CAS.DTOs;
using CAS.DTOs.Season;
using CAS.Interfaces.Repositories;
using CAS.Interfaces.Services;
using CAS.Models.Entities;

namespace CAS.Implementation.Services
{
    public class SeasonService : ISeasonService
    {
        private readonly ISeasonRepository _seasonRepository;

        public SeasonService(ISeasonRepository seasonRepository)
        {
            _seasonRepository = seasonRepository ?? throw new ArgumentNullException(nameof(seasonRepository));
        }
        public async Task<BaseResponse<SeasonDto>> GetActiveSeasonById(Guid id)
        {
            var season = await _seasonRepository.Get<Season>(s => s.Id == id);
            if(season == null)
            {
                return new BaseResponse<SeasonDto>
                {
                    IsSuccess = false,
                    Message = "Season not found."
                };
            }

            var seasonResponse = new SeasonDto
            {
                Id = season.Id,
                Name = season.Name,
                SeasonStatus = season.SeasonStatus,
                CreatedAt = DateTime.UtcNow,
            };

            return new BaseResponse<SeasonDto> { IsSuccess = true, Message = "Season fetched successfully", Data = seasonResponse };
        }

        public async Task<BaseResponse<IReadOnlyList<SeasonListResponseModel>>> GetAllActiveSeasonsAsync()
        {
            var seasons = await _seasonRepository.GetAll<Season>();
            if(seasons == null)
            {
                return new BaseResponse<IReadOnlyList<SeasonListResponseModel>>
                {
                    IsSuccess = false,
                    Message = "No seasons found.",

                };
            }

            var seasonListResponse = seasons.Select(s => new SeasonListResponseModel
            {
                Id = s.Id,
                Name = s.Name,
                SeasonStatus = s.SeasonStatus,
            }).ToList();

            return new BaseResponse<IReadOnlyList<SeasonListResponseModel>>
            {
                IsSuccess = true,
                Message = $"{seasons.Count} fetched successful",
                Data = seasonListResponse
            };
        }

        public async Task<BaseResponse<IReadOnlyList<SeasonListResponseModel>>> GetAllSeasonsForAdminAsync()
        {
            var seasons = await _seasonRepository.GetAllSeasonsForAdmin();
            if (seasons == null)
            {
                return new BaseResponse<IReadOnlyList<SeasonListResponseModel>>
                {
                    IsSuccess = false,
                    Message = "No seasons found.",

                };
            }

            var seasonListResponse = seasons.Select(s => new SeasonListResponseModel
            {
                Id = s.Id,
                Name = s.Name,
                SeasonStatus = s.SeasonStatus,
            }).ToList();

            return new BaseResponse<IReadOnlyList<SeasonListResponseModel>>
            {
                IsSuccess = true,
                Message = $"{seasons.Count} fetched successful",
                Data = seasonListResponse
            };
        }

        public async Task<BaseResponse<SeasonDto>> GetSeasonForAdminAsync(Guid id)
        {
            var season = await _seasonRepository.GetSeasonByIdForAdmin(id);
            if (season == null)
            {
                return new BaseResponse<SeasonDto>
                {
                    IsSuccess = false,
                    Message = "Season not found."
                };
            }

            var seasonResponse = new SeasonDto
            {
                Id = season.Id,
                Name = season.Name,
                SeasonStatus = season.SeasonStatus,
                CreatedAt = DateTime.UtcNow,
            };

            return new BaseResponse<SeasonDto> { IsSuccess = true, Message = "Season fetched successfully", Data = seasonResponse };
        }
    }
    
}
