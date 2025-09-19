using MXC.Domain.DataTransferObjects;
using MXC.Domain.DataTransferObjects.EventManagement;
using MXC.Shared.ResultType;

namespace MXC.Application.Services.EventManagementService;

public interface IEventManagementService
{
    Task<Result> CreateEventItem(EventItemCreateDTO eventItemCreate, CancellationToken cancellationToken);

    Task<Result> UpdateEventItem(EventItemDTO eventItem, CancellationToken cancellationToken);

    Task<Result> DeleteEventItem(int eventId, CancellationToken cancellationToken);

    Task<Result<EventItemDTO>> GetEventItemByEventId(int eventId, CancellationToken cancellationToken);

    Task<Result<PaginationWrapperDTO<EventManagementItemDTO>>> GetEventManagementItems(EventManagementFilterDTO eventManagementFilter, CancellationToken cancellationToken);
}