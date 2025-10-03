using MXC.Domain.DataTransferObjects.Common;

namespace MXC.Domain.DataTransferObjects.Country;

public class CountryItemFilterDTO : PaginationBaseDTO
{
    public string SearchText { get; set; } = string.Empty;
}