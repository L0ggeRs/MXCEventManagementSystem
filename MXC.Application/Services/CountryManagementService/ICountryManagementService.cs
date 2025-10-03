using MXC.Domain.DataTransferObjects.Common;
using MXC.Domain.DataTransferObjects.Country;
using MXC.Shared.ResultType;

namespace MXC.Application.Services.CountryManagementService;

public interface ICountryManagementService
{
    Task<Result<PaginationWrapperDTO<CountryManagementItemDTO>>> GetCountryItems(CountryItemFilterDTO countryItemFilter, CancellationToken cancellationToken);
}