using Microsoft.EntityFrameworkCore;
using MXC.Domain.DataTransferObjects.Common;
using MXC.Domain.DataTransferObjects.Country;
using MXC.Domain.DataTransferObjects.EventManagement;
using MXC.Domain.Entities;
using MXC.Domain.Enums;
using MXC.Infrastructure.Context;
using MXC.Infrastructure.Repositories.RepositoryBase.NoTrackingRepositoryBase;
using MXC.Shared;

namespace MXC.Infrastructure.Repositories.NoTracking.CountriesRepository;

public class CountriesNoTrackingRepository(ApplicationNoTrackingDbContext context) : NoTrackingRepositoryBase<CountryEntity>(context), ICountriesNoTrackingRepository
{
    public async Task<IReadOnlyCollection<EventCountryDTO>> GetEventCountries(SearchTextDTO searchText, CancellationToken cancellationToken)
    {
        Ensure.NotNull(searchText);

        var searchQuery = FindAll()
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchText.SearchText))
        {
            searchQuery = searchQuery.Where(x => x.CountryName.Contains(searchText.SearchText));
        }

        return await searchQuery
            .Select(x => new EventCountryDTO()
            {
                CountryId = x.Id,
                CountryName = x.CountryName
            })
            .OrderBy(x => x.CountryName)
            .ToListAsync(cancellationToken);
    }

    public async Task<PaginationWrapperDTO<CountryManagementItemDTO>> FindCountriesForManagement(CountryItemFilterDTO countryItemFilter, CancellationToken cancellationToken)
    {
        Ensure.NotNull(countryItemFilter);

        var isAscending = countryItemFilter.OrderDirection == OrderDirection.Asc;
        var searchQuery = FindByCondition(c => c.CountryName.Contains(countryItemFilter.SearchText))
            .Select(c => new CountryManagementItemDTO()
            {
                CountryId = c.Id,
                CountryName = c.CountryName
            })
            .AsQueryable();

        searchQuery = isAscending ? searchQuery.OrderBy(x => x.CountryName) : searchQuery.OrderByDescending(x => x.CountryName);

        var searchResultCount = await searchQuery.CountAsync(cancellationToken);
        var items = await searchQuery
            .Skip(countryItemFilter.PageNumber * countryItemFilter.ItemsOnPage)
            .Take(countryItemFilter.ItemsOnPage)
            .ToListAsync(cancellationToken);

        return new PaginationWrapperDTO<CountryManagementItemDTO>()
        {
            ItemCount = searchResultCount,
            Items = items,
            PageNumber = countryItemFilter.PageNumber,
            ItemsOnPage = countryItemFilter.ItemsOnPage
        };
    }
}