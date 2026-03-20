namespace PaymentProcessingSystem.Application.Results;

public class OperationResult<T> : OperationResult
{
    public T? Data { get; set; }

    public static OperationResult<T> Success(string message, T data)
    {
        return new OperationResult<T>
        {
            IsSuccess = true,
            Message = message,
            Data = data
        };
    }

    public static new OperationResult<T> Failure(string message)
    {
        return new OperationResult<T>
        {
            IsSuccess = false,
            Message = message,
            Data = default
        };
    }
}