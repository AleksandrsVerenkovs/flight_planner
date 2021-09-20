using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flight_Planner.Models;
using Flight_Planner.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Flight_Planner.Controllers
{
    [Authorize]
    [ApiController]
    [Route("admin-api")]
    public class AdminController : ControllerBase
    {
        private static object _lock = new object(); 
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

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(Flight flight)
        {
            lock (_lock)
            {
                if (FlightList.Exists(flight))
                {
                    return Conflict();
                }
                if (!FlightList.IsValid(flight))
                {
                    return BadRequest();
                }
                FlightList.AddFlight(flight);
                return Created("", flight);
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (_lock)
            {
                FlightList.DeleteFlight(id);
                return Ok();
            }
        }
    }
}
