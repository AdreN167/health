namespace Health.Domain.Models.Response;

public class BaseResponse<T>
{
    public int ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public bool ISuccessful => string.IsNullOrWhiteSpace(ErrorMessage);
    public T Data { get; set; }

    public BaseResponse() { }

    public BaseResponse(int errorCode, string errorMessage, T data)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        Data = data;
    }
}

