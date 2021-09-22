using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Flight_Planner.Repository;

namespace Flight_Planner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly FlightList _flightList = null;
        public TestingController(FlightList flightList)
        {
            _flightList = flightList;
        }
        [HttpPost]
        [Route("clear")]
        public IActionResult PostResult()
        {
            _flightList.ClearFlight();
            return Ok();

        }
    }
}
