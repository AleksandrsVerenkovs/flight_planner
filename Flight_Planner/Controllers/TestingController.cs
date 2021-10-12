using Microsoft.AspNetCore.Mvc;
using FlightPlanner.Core.Services;
using FlightPlanner.Core.Models;

namespace Flight_Planner.Controllers
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        private readonly IDbServiceExtended _service;
        public TestingController(IDbServiceExtended service)
        {
            _service = service;
        }
        [HttpPost]
        [Route("clear")]
        public IActionResult PostResult()
        {
            _service.DeleteAll<Flight>();
            _service.DeleteAll<Airport>();
            return Ok();

        }
    }
}
