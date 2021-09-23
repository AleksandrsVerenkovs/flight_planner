using Flight_Planner.Models;
using Flight_Planner.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Flight_Planner.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly FlightList _flightList = null;
        public CustomerController(FlightList flightList)
        {
            _flightList = flightList;
        }
        [HttpGet]
        [Route("airports")]
        public IActionResult GetByTag(string search)
        {
            return Ok(_flightList.GetByTag(search));
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(FlightSearch search)
        {
            if (!_flightList.IsValidFlight(search))
                return BadRequest();
            if (!_flightList.IsSameAirport(search))
                return BadRequest();

            var response = _flightList.SearchResult(search);
            return Ok(response);
        }
        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flightId = _flightList.GetById(id);
            if (flightId == null)
            {
                return NotFound();
            }

            return Ok(flightId);
        }
    }
}
