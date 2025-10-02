namespace MXC.Infrastructure.Models;

public class EventManagementModel
{
    public int EventId { get; set; }
    public string EventName { get; set; } = string.Empty;
    public int LocationId { get; set; }
    public uint? Capacity { get; set; }
    public string LocationName { get; set; } = string.Empty;
    public string CountryName { get; set; } = string.Empty;
}