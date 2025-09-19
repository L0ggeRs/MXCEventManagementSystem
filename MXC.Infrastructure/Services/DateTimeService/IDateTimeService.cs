namespace MXC.Infrastructure.Services.DateTimeService;

public interface IDateTimeService
{
    DateTime UtcNow { get; }
    DateOnly UtcDateNow { get; }
}