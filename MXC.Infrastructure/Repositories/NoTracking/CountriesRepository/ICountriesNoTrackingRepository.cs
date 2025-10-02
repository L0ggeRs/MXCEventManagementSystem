using MXC.Domain.DataTransferObjects.EventManagement;
using MXC.Domain.Entities;
using MXC.Infrastructure.Repositories.RepositoryBase.NoTrackingRepositoryBase;

namespace MXC.Infrastructure.Repositories.NoTracking.CountriesRepository;

public interface ICountriesNoTrackingRepository : INoTrackingRepositoryBase<CountryEntity>
{
    IQueryable<CountryEntity> GetAllCountry();

    Task<IReadOnlyCollection<EventCountryDTO>> GetEventCountries(IQueryable<CountryEntity> searchQuery, CancellationToken cancellationToken);
}