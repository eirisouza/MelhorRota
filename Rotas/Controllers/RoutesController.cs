using Mapster;
using Microsoft.AspNetCore.Mvc;
using Routes.Api.Models;
using Routes.Domain.Entities;
using Routes.Domain.Interfaces.Services;

namespace Routes.Api.Controllers;

[Route("api/routes")]
[ApiController]
public sealed class RoutesController(IRouteService rotasService) : ControllerBase
{
    private readonly IRouteService rotasService = rotasService;

    [HttpGet]
    [ProducesResponseType(typeof(List<RouteModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get()
    {
        var routes = await rotasService.GetAllAsync();

        if (routes.Count > 0)
        {
            return Ok(routes.Adapt<List<RouteModel>>());
        }

        return NotFound();
    }

    [HttpGet("target/{target}")]
    [ProducesResponseType(typeof(RouteModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByTarger(string target)
    {
        var rota = await rotasService.GetByTargetAsync(target);

        if (rota is not null)
        {
            return Ok(rota);
        }

        return NotFound();
    }

    [HttpGet("source/{source}")]
    [ProducesResponseType(typeof(RouteModel), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBySource(string source)
    {
        var rota = await rotasService.GetBySourceAsync(source);

        if (rota is not null)
        {
            return Ok(rota);
        }

        return NotFound();
    }

    [HttpGet("better")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBetterRoute([FromQuery] BetterRouterRequestModel request)
    {
        var rota = await rotasService.GetBetterRouteAsync(request.Source, request.Target);

        if (rota is not null)
        {
            return Ok(rota);
        }

        return NotFound();
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Post(RouteModel rota)
    {
        await rotasService.AddAsync(rota.Adapt<Domain.Entities.Route>());

        return NoContent();
    }
}
