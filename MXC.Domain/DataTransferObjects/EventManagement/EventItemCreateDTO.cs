namespace MXC.Domain.DataTransferObjects.EventManagement;

public class EventItemCreateDTO
{
    public string EventName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string? Country { get; set; }
    public uint? Capacity { get; set; }
}