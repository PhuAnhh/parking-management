namespace Application.Contracts;

public class BaseResponse(bool isSuccess, string message)
{
    public bool IsSuccess { get; set; } = isSuccess;

    public string Message { get; set; } = message;

    public static BaseResponse Success => new BaseResponse(true, (string)null);

    public static BaseResponse Failed => new BaseResponse(false, (string)null);
}