namespace MXC.Domain.Entities;

public class EventEntity : BaseEntity
{
    public string EventName { get; set; } = string.Empty;
    public int LocationId { get; set; }
    public virtual LocationEntity Location { get; set; } = null!;
    public int? CountryId { get; set; }
    public virtual CountryEntity? Country { get; set; }
    public uint? Capacity { get; set; }
}