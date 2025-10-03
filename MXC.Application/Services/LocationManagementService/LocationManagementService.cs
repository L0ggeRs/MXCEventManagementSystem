using MXC.Domain.DataTransferObjects.Common;
using MXC.Domain.DataTransferObjects.Location;
using MXC.Infrastructure.Repositories.NoTracking.LocationsRepository;
using MXC.Shared.Enum;
using MXC.Shared.ResultType;

namespace MXC.Application.Services.LocationManagementService;

public class LocationManagementService(ILocationsNoTrackingRepository locationsNoTrackingRepository) : ILocationManagementService
{
    private readonly ILocationsNoTrackingRepository _locationsNoTrackingRepository = locationsNoTrackingRepository;

    public async Task<Result<PaginationWrapperDTO<LocationManagementItemDTO>>> GetLocationItems(LocationItemFilterDTO locationItemFilterDTO, CancellationToken cancellationToken)
    {
        if (locationItemFilterDTO is null)
        {
            return Result<PaginationWrapperDTO<LocationManagementItemDTO>>.Failure(ErrorType.NotSet);
        }

        var result = await _locationsNoTrackingRepository.FindLocationsForManagement(locationItemFilterDTO, cancellationToken);

        return Result<PaginationWrapperDTO<LocationManagementItemDTO>>.Success(result);
    }
}