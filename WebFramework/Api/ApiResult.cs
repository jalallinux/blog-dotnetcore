
namespace WebFramework.Api;

public class ApiResult
{
    public bool IsSuccess { get; set; } = true;
    public ApiResultStatusCode StatusCode { get; set; } = ApiResultStatusCode.Success;
    public string Message { get; set; } = "Operation successful.";
}

public class ApiResult<TData> : ApiResult
{
    public TData Data { get; set; }
}

