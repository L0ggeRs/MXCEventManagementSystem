using MXC.Shared.Enum;

namespace MXC.Shared.ResultType;

public class Result : ResultBase
{
    protected internal Result() : base()
    {
    }

    protected internal Result(ErrorType error) : base(error)
    {
    }

    protected internal Result(ErrorType error, IEnumerable<string> validationErrors) : base(error, validationErrors)
    {
    }

    public static Result Success() => new();

    public static Result Failure(ErrorType error) => new(error);

    public static Result Failure(ErrorType error, IEnumerable<string> validationErrors) => new(error, validationErrors);
}