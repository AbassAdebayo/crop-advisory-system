using CAS.Contracts.Identity;
using CAS.DTOs;
using CAS.DTOs.Auth;
using CAS.Interfaces.Repositories;
using CAS.Interfaces.Services;
using CAS.Models.Entities;

namespace CAS.Implementation.Services
{
    public class UserService(IUserRepository userRepository, IIdentityService identityService, IRoleRepository roleRepository,
        ILogger<UserService> logger, IUnitOfWork unitOfWork) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        private readonly IRoleRepository _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
        private readonly ILogger<UserService> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        public async Task<BaseResponse> RegisterFarmerAsync(RegisterFarmerRequestModel model)
        {
            var farmerExists = await _userRepository.Any<User>(u => u.Email == model.Email);
            if(farmerExists) return new BaseResponse
            {
                IsSuccess = false,
                Message = "Farmer with this email already exists.",
            };

            var farmerRole = await _roleRepository.Get<Role>(r => r.Name == "Farmer");
            if (farmerRole is null) return new BaseResponse
            {
                Message = "Role doesn't exist",
                IsSuccess = false,
            };

            var hashPassword = _identityService.GetPasswordHash(model.PasswordHash);

            var normalizeEmail = _identityService.GetNormalizedEmail(model.Email);

            var farmerAccount = new User
            {
                Email = normalizeEmail,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Location = model.Location,
                PasswordHash = hashPassword,
                RoleId = farmerRole.Id,
                CreatedAt = DateTime.UtcNow,
            };

            var addAccount = await _userRepository.Add<User>(farmerAccount);
            var result = await _unitOfWork.SaveChangesAsync();
            if(result > 0)
            {
                _logger.LogInformation("Farmer account created successfully for {Email}", model.Email);
                return new BaseResponse
                {
                    IsSuccess = true,
                    Message = "Farmer account created successfully.",
                };
            }

            _logger.LogError("Failed to create farmer account for {Email}", model.Email);
            return new BaseResponse
            {
                IsSuccess = false,
                Message = "Failed to create farmer account.",
            };
        }
    }
}
