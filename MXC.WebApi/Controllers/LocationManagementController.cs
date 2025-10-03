using Microsoft.AspNetCore.Mvc;
using MXC.Application.Services.LocationManagementService;
using MXC.Domain.DataTransferObjects.Location;
using MXC.Shared.Enum;

namespace MXC.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationManagementController(ILocationManagementService locationManagementService) : ControllerBase
{
    [HttpGet("locations")]
    public async Task<IActionResult> GetLocationItems([FromQuery] LocationItemFilterDTO locationItemFilterDTO, CancellationToken cancellationToken)
    {
        var result = await locationManagementService.GetLocationItems(locationItemFilterDTO, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return result.Error switch
        {
            ErrorType.NotSet => BadRequest(),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }
}