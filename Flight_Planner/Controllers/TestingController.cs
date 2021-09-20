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
        [HttpPost]
        [Route("clear")]
        public IActionResult PostResult()
        {
            FlightList.ClearFlight();
            return Ok();

        }
    }
}
