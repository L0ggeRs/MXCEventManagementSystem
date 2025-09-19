namespace MXC.Domain.DataTransferObjects.EventManagement;

public class EventManagementItemDTO
{
    public int EventId { get; set; }
    public string EventName { get; set; } = string.Empty;
    public string Place { get; set; } = string.Empty;
    public uint? Capacity { get; set; }
}