using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Flight_Planner.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IFlightSearchService _flightSearchService;
        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        private readonly IPageResultService _pageResultService;
        private readonly ISearchValidator _airportSearchValidation;

        public CustomerController(
            IFlightSearchService flightSearchService, 
            IMapper mapper, 
            IPageResultService pageResultService,
            ISearchValidator airportSearchValidation,
            IFlightService flightService)
        {
            _flightSearchService= flightSearchService;
            _mapper = mapper;
            _pageResultService = pageResultService;
            _airportSearchValidation = airportSearchValidation;
            _flightService = flightService;
        }
        [HttpGet]
        [Route("airports")]
        public IActionResult GetByTag(string search)
        {
            var airport = _flightSearchService.GetByTag(search);
            return Ok(new[] { _mapper.Map<AirportResponse>(airport) });
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(FlightSearch request)
        {

            if (!_airportSearchValidation.IsValid(request) || request.To == request.From)
            {
                return BadRequest();
            }

            var response = _pageResultService.GetPageResult(request);
           
            return Ok(response);
        }
        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flight = _flightService.GetFlightById(id);
            if (flight == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<FlightResponse>(flight));

        }
    }
}
