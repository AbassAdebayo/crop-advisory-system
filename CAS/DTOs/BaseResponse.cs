namespace CAS.DTOs
{
    public class BaseResponse
    {
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }

    }

    public class BaseResponse<T> : BaseResponse
    {
        public T? Data { get; set; }
    }
}
