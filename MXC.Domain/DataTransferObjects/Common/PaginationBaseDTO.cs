using MXC.Domain.Enums;

namespace MXC.Domain.DataTransferObjects.Common;

public class PaginationBaseDTO
{
    public OrderDirection OrderDirection { get; set; }
    public int PageNumber { get; set; }
    public int ItemsOnPage { get; set; }
}