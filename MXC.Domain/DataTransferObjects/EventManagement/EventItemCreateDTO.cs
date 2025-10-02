namespace MXC.Domain.DataTransferObjects.EventManagement;

public class EventItemCreateDTO
{
    public string EventName { get; set; } = string.Empty;
    public int LocationId { get; set; }
    public int? CountryId { get; set; }
    public uint? Capacity { get; set; }
}