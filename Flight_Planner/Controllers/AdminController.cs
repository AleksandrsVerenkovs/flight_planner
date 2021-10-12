using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Dto;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Flight_Planner.Controllers
{
    [Authorize]
    [ApiController]
    [Route("admin-api")]
    public class AdminController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IValidator> _validators;
        private static object _lock = new object();

        public AdminController(IFlightService flightService, IMapper mapper, IEnumerable<IValidator> validators)
        {
            _flightService = flightService;
            _mapper = mapper;
            _validators = validators;
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flightId = _flightService.GetFlightById(id);
            if (flightId == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FlightResponse>(flightId));
        }

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(FlightRequest request)
        {
            lock (_lock)
            {
                var flight = _mapper.Map<Flight>(request);

                if (!_validators.All(s => s.IsValid(request)))
                {
                    return BadRequest();
                }
                if (_flightService.Exists(flight))
                {
                    return Conflict();
                }
                _flightService.Create(flight);
                return Created("", _mapper.Map<FlightResponse>(flight));
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (_lock)
            {
                var flight = _flightService.GetFlightById(id);
                try
                {
                    _flightService.Delete(flight);
                }
                catch
                {
                    return Ok();
                }
                return Ok();
            }
        }
    }
}
