using CAS.DTOs;
using CAS.DTOs.Auth;

namespace CAS.Interfaces.Services
{
    public interface IUserService
    {
        public Task<BaseResponse> RegisterFarmerAsync(RegisterFarmerRequestModel model);

    }
}
