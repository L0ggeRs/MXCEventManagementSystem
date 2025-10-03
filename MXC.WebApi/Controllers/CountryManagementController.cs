using Microsoft.AspNetCore.Mvc;
using MXC.Application.Services.CountryManagementService;
using MXC.Domain.DataTransferObjects.Country;
using MXC.Shared.Enum;

namespace MXC.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryManagementController(ICountryManagementService countryManagementService) : ControllerBase
{
    [HttpGet("countries")]
    public async Task<IActionResult> GetCountryItems([FromQuery] CountryItemFilterDTO countryItemFilter, CancellationToken cancellationToken)
    {
        var result = await countryManagementService.GetCountryItems(countryItemFilter, cancellationToken);

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