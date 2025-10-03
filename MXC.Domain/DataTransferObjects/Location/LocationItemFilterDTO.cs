using MXC.Domain.DataTransferObjects.Common;

namespace MXC.Domain.DataTransferObjects.Location;

public class LocationItemFilterDTO : PaginationBaseDTO
{
    public string SearchText { get; set; } = string.Empty;
}