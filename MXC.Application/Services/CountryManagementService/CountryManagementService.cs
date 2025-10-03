using MXC.Domain.DataTransferObjects.Common;
using MXC.Domain.DataTransferObjects.Country;
using MXC.Infrastructure.Repositories.NoTracking.CountriesRepository;
using MXC.Shared.Enum;
using MXC.Shared.ResultType;

namespace MXC.Application.Services.CountryManagementService;

public class CountryManagementService(ICountriesNoTrackingRepository countriesNoTrackingRepository) : ICountryManagementService
{
    private readonly ICountriesNoTrackingRepository _countriesNoTrackingRepository = countriesNoTrackingRepository;

    public async Task<Result<PaginationWrapperDTO<CountryManagementItemDTO>>> GetCountryItems(CountryItemFilterDTO countryItemFilter, CancellationToken cancellationToken)
    {
        if (countryItemFilter is null)
        {
            return Result<PaginationWrapperDTO<CountryManagementItemDTO>>.Failure(ErrorType.NotSet);
        }

        var result = await _countriesNoTrackingRepository.FindCountriesForManagement(countryItemFilter, cancellationToken);

        return Result<PaginationWrapperDTO<CountryManagementItemDTO>>.Success(result);
    }
}