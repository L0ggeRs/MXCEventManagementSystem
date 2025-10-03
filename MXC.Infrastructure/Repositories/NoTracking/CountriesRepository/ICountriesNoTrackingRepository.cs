using MXC.Domain.DataTransferObjects.Common;
using MXC.Domain.DataTransferObjects.Country;
using MXC.Domain.DataTransferObjects.EventManagement;
using MXC.Domain.Entities;
using MXC.Infrastructure.Repositories.RepositoryBase.NoTrackingRepositoryBase;

namespace MXC.Infrastructure.Repositories.NoTracking.CountriesRepository;

public interface ICountriesNoTrackingRepository : INoTrackingRepositoryBase<CountryEntity>
{
    Task<IReadOnlyCollection<EventCountryDTO>> GetEventCountries(SearchTextDTO searchText, CancellationToken cancellationToken);

    Task<PaginationWrapperDTO<CountryManagementItemDTO>> FindCountriesForManagement(CountryItemFilterDTO countryItemFilter, CancellationToken cancellationToken);
}