using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SecureFlight.Api.Models;
using SecureFlight.Api.Utils;
using SecureFlight.Core.Entities;
using SecureFlight.Core.Interfaces;

namespace SecureFlight.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightsController(IService<Flight> flightService, IMapper mapper)
    : SecureFlightBaseController(mapper)
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<FlightDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> Get()
    {
        var flights = await flightService.GetAllAsync();
        return MapResultToDataTransferObject<IReadOnlyList<Flight>, IReadOnlyList<FlightDataTransferObject>>(flights);
    }

    [HttpGet("/origin/{origin}/destination/{destination}")]
    [ProducesResponseType(typeof(IEnumerable<FlightDataTransferObject>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseActionResult))]
    public async Task<IActionResult> GetByOriginAndDestination(string origin, string destination)
    {
        var flights = await flightService.GetAllAsync();
        var filteredFlights = flights.Result.Where(f => f.OriginAirport == origin && f.DestinationAirport == destination);
        return Ok(filteredFlights);
    }
}