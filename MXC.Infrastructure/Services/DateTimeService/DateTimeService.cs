namespace MXC.Infrastructure.Services.DateTimeService;

public class DateTimeService : IDateTimeService
{
    public DateTime UtcNow => DateTime.UtcNow.ToLocalTime();

    public DateOnly UtcDateNow => DateOnly.FromDateTime(UtcNow);
}