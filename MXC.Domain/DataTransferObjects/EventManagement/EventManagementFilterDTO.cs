using MXC.Domain.DataTransferObjects.Common;
using MXC.Domain.Enums;

namespace MXC.Domain.DataTransferObjects.EventManagement;

public class EventManagementFilterDTO : PaginationBaseDTO
{
    public EventManagementOrderBy EventManagementOrderBy { get; set; }
}