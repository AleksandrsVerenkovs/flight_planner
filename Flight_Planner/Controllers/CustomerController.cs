using Flight_Planner.Models;
using Flight_Planner.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flight_Planner.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpGet]
        [Route("airports")]

        public IActionResult GetByTag(string search)
        {
            return Ok(FlightList.GetByTag(search));
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(FlightSearch search)
        {
            if (!FlightList.IsValidFlight(search))
                return BadRequest();
            if (!FlightList.IsSameAirport(search))
                return BadRequest();

            var response = FlightList.SearchResult(search);
            return Ok(response);
        }
        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult GetFlight(int id)
        {
            var flightId = FlightList.GetById(id);
            if (flightId == null)
            {
                return NotFound();
            }

            return Ok(flightId);
        }
    }
}
