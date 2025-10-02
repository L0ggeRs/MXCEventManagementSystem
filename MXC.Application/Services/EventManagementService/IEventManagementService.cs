using MXC.Domain.DataTransferObjects.Common;
using MXC.Domain.DataTransferObjects.EventManagement;
using MXC.Shared.ResultType;

namespace MXC.Application.Services.EventManagementService;

public interface IEventManagementService
{
    Task<Result> CreateEventItem(EventItemCreateDTO eventItemCreate, CancellationToken cancellationToken);

    Task<Result> UpdateEventItem(EventItemUpdateDTO eventItem, CancellationToken cancellationToken);

    Task<Result> DeleteEventItem(int eventId, CancellationToken cancellationToken);

    Task<Result<EventItemDTO>> GetEventItemByEventId(int eventId, CancellationToken cancellationToken);

    Task<Result<PaginationWrapperDTO<EventManagementItemDTO>>> GetEventManagementItems(EventManagementFilterDTO eventManagementFilter, CancellationToken cancellationToken);

    Task<Result<IReadOnlyCollection<EventCountryDTO>>> FilterCountries(SearchTextDTO searchText, CancellationToken cancellationToken);

    Task<Result<IReadOnlyCollection<EventLocationDTO>>> FilterLocations(SearchTextDTO searchText, CancellationToken cancellationToken);
}