using MXC.Domain.DataTransferObjects.Common;
using MXC.Domain.DataTransferObjects.Location;
using MXC.Shared.ResultType;

namespace MXC.Application.Services.LocationManagementService;

public interface ILocationManagementService
{
    Task<Result<PaginationWrapperDTO<LocationManagementItemDTO>>> GetLocationItems(LocationItemFilterDTO locationItemFilterDTO, CancellationToken cancellationToken);
}