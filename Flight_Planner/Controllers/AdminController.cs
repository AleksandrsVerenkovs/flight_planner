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
        private readonly FlightList _flightList = null;
        public AdminController(FlightList flightList)
        {
            _flightList = flightList;
        }

        private static object _lock = new object(); 
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

        [HttpPut]
        [Route("flights")]
        public IActionResult PutFlight(Flight flight)
        {
            lock (_lock)
            {
                if (!_flightList.IsValid(flight))
                {
                    return BadRequest();
                }
                if (_flightList.Exists(flight))
                {
                    return Conflict();
                }
                
                _flightList.AddFlight(flight);
                return Created("", flight);
            }
        }

        [HttpDelete]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (_lock)
            {
                _flightList.DeleteFlight(id);
                return Ok();
            }
        }
    }
}
