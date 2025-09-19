using MXC.Shared.Enum;

namespace MXC.Shared.ResultType;

public class Result<TValue> : ResultBase
{
    private readonly TValue? _value;

    protected internal Result(TValue? value) : base()
    {
        if (value is null)
        {
            throw new InvalidOperationException("The value can't be null.");
        }
        _value = value;
    }

    protected internal Result(ErrorType error) : base(error)
    {
    }

    protected internal Result(ErrorType error, IReadOnlyCollection<string> validationErrors) : base(error, validationErrors)
    {
    }

    public TValue Value => IsSuccess
                ? _value!
        : throw new InvalidOperationException("The value of a failure result can't be accessed.");

    public static Result<TValue> Success(TValue value) => new(value);

    public static Result<TValue> Failure(ErrorType error) => new(error);

    public static Result<TValue> Failure(ErrorType error, IReadOnlyCollection<string> validationErrors) => new(error, validationErrors);
}