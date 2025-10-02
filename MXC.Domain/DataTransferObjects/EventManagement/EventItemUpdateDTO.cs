namespace MXC.Domain.DataTransferObjects.EventManagement;

public class EventItemUpdateDTO
{
    public int EventId { get; set; }
    public string EventName { get; set; } = string.Empty;
    public int LocationId { get; set; }
    public int? CountryId { get; set; }
    public uint? Capacity { get; set; }
}