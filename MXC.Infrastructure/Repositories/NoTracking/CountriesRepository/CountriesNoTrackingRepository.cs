using Microsoft.EntityFrameworkCore;
using MXC.Domain.DataTransferObjects.EventManagement;
using MXC.Domain.Entities;
using MXC.Infrastructure.Context;
using MXC.Infrastructure.Repositories.RepositoryBase.NoTrackingRepositoryBase;

namespace MXC.Infrastructure.Repositories.NoTracking.CountriesRepository;

public class CountriesNoTrackingRepository(ApplicationNoTrackingDbContext context) : NoTrackingRepositoryBase<CountryEntity>(context), ICountriesNoTrackingRepository
{
    public IQueryable<CountryEntity> GetAllCountry()
    {
        return FindAll()
            .AsQueryable();
    }

    public async Task<IReadOnlyCollection<EventCountryDTO>> GetEventCountries(IQueryable<CountryEntity> searchQuery, CancellationToken cancellationToken)
    {
        return await searchQuery
            .Select(x => new EventCountryDTO()
            {
                CountryId = x.Id,
                CountryName = x.CountryName
            })
            .OrderBy(x => x.CountryName)
            .ToListAsync(cancellationToken);
    }
}