using MXC.Shared.Enum;

namespace MXC.Shared.ResultType;

public abstract class ResultBase
{
    private readonly ErrorType? _error;

    protected ResultBase()
    {
        IsSuccess = true;
    }

    protected ResultBase(ErrorType error)
    {
        IsSuccess = false;
        _error = error;
    }

    protected ResultBase(ErrorType error, IEnumerable<string> validationErrors) : this(error)
    {
        IsSuccess = false;
        _error = error;
        ValidationErrors = validationErrors;
    }

    public ErrorType Error => IsSuccess
        ? throw new InvalidOperationException("The error of a success result can't be accessed.")
        : _error!.Value;

    public IEnumerable<string> ValidationErrors { get; } = [];
    public bool IsFailure => !IsSuccess;
    public bool IsSuccess { get; protected set; }
}