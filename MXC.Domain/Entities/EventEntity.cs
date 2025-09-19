namespace MXC.Domain.Entities;

public class EventEntity : BaseEntity
{
    public string EventName { get; set; } = string.Empty;
    public string EventLocation { get; set; } = string.Empty;
    public string? Country { get; set; }
    public uint? Capacity { get; set; }
}