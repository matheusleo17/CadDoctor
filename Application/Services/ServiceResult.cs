public class ServiceResult<T>
{
    public bool Success { get;  set; }
    public string ErrorMessage { get;  set; }
    public T Data { get;  set; }
    public  T? Value { get; set; }

    public string? StateCode { get; set; }
    public string AddMessage { get; set; }

    public ServiceResult(bool success, T value, string errorMessage, string AddMessage, string? stateCode)
    {
        Success = success;
        Value = value;
        ErrorMessage = errorMessage;
        StateCode = stateCode;
    }

    public ServiceResult() { }

    public ServiceResult<T> Ok(T data)
    {
        Success = true;
        Data = data;
        ErrorMessage = null;
        return this;
    }

    public ServiceResult<T> Fail(string message)
    {
        Success = false;
        Data = default;
        ErrorMessage = message;
        return this;
    }

}
