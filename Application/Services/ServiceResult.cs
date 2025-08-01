public class ServiceResult<T>
{
    public bool Success { get; private set; }
    public string ErrorMessage { get; private set; }
    public T Data { get; private set; }

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
