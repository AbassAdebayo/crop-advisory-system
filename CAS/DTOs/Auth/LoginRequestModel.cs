namespace CAS.DTOs.Auth
{
    public class LoginRequestModel
    {
        public required string Email{ get; set; }
        public required string Password{ get; set; }
    }

    public class LoginResponseData
    {
       public Guid UserId { get; set; }
       public string Role { get; set; } = string.Empty;

       public string FullName = string.Empty;

   }

    public class LoginResponseModel : BaseResponse
    {
        public LoginResponseData Data { get; set; } = null!;
    }
}
