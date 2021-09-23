using Microsoft.AspNetCore.Mvc;
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
