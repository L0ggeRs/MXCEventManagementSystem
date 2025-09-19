namespace MXC.Domain.DataTransferObjects.EventManagement;

public class EventItemDTO
{
    public int EventId { get; set; }
    public string EventName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string? Country { get; set; }
    public uint? Capacity { get; set; }
}