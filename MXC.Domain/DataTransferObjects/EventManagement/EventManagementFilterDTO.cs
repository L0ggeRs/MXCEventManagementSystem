using MXC.Domain.Enums;

namespace MXC.Domain.DataTransferObjects.EventManagement;

public class EventManagementFilterDTO
{
    public OrderDirection OrderDirection { get; set; }
    public EventManagementOrderBy EventManagementOrderBy { get; set; }
    public int PageNumber { get; set; }
    public int ItemsOnPage { get; set; }
}