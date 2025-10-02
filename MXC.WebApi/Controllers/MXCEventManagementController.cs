using Microsoft.AspNetCore.Mvc;
using MXC.Application.Services.EventManagementService;
using MXC.Domain.DataTransferObjects.Common;
using MXC.Domain.DataTransferObjects.EventManagement;
using MXC.Shared.Enum;

namespace MXC.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MXCEventManagementController(IEventManagementService eventManagementService) : ControllerBase
{
    [HttpGet("events")]
    public async Task<IActionResult> GetEventManagementItems([FromQuery] EventManagementFilterDTO eventManagementFilter, CancellationToken cancellationToken)
    {
        var result = await eventManagementService.GetEventManagementItems(eventManagementFilter, cancellationToken);

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

    [HttpGet("event/{eventId}")]
    public async Task<IActionResult> GetEventManagementItemById([FromRoute] int eventId, CancellationToken cancellationToken)
    {
        var result = await eventManagementService.GetEventItemByEventId(eventId, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return result.Error switch
        {
            ErrorType.NotFound => NotFound(),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    [HttpPut("event")]
    public async Task<IActionResult> UpdateEventItem([FromBody] EventItemUpdateDTO eventItem, CancellationToken cancellationToken)
    {
        var result = await eventManagementService.UpdateEventItem(eventItem, cancellationToken);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        return result.Error switch
        {
            ErrorType.Validation => BadRequest(result.ValidationErrors),
            ErrorType.NotSet => BadRequest(),
            ErrorType.NotFound => NotFound(),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    [HttpPost("event")]
    public async Task<IActionResult> CreateEventItem([FromBody] EventItemCreateDTO eventItemCreate, CancellationToken cancellationToken)
    {
        var result = await eventManagementService.CreateEventItem(eventItemCreate, cancellationToken);

        if (result.IsSuccess)
        {
            return Created();
        }

        return result.Error switch
        {
            ErrorType.Validation => BadRequest(result.ValidationErrors),
            ErrorType.NotSet => BadRequest(),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    [HttpDelete("event/{eventId}")]
    public async Task<IActionResult> DeleteEventItem([FromRoute] int eventId, CancellationToken cancellationToken)
    {
        var result = await eventManagementService.DeleteEventItem(eventId, cancellationToken);

        if (result.IsSuccess)
        {
            return NoContent();
        }

        return result.Error switch
        {
            ErrorType.NotFound => NotFound(),
            _ => StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    [HttpGet("event/countries")]
    public async Task<IActionResult> FilterCountries([FromQuery] SearchTextDTO searchText, CancellationToken cancellationToken)
    {
        var result = await eventManagementService.FilterCountries(searchText, cancellationToken);

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

    [HttpGet("event/locations")]
    public async Task<IActionResult> FilterLocations([FromQuery] SearchTextDTO searchText, CancellationToken cancellationToken)
    {
        var result = await eventManagementService.FilterLocations(searchText, cancellationToken);

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