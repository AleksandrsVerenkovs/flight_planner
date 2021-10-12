using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using System.Collections.Generic;
using System.Linq;

namespace FlightPlanner.Services
{
    public class PageResultService : EntityService<Flight>, IPageResultService
    {
        public PageResultService(IFlightPlannerDbContext context ) : base(context)
        {
        }
        public PageResult GetPageResult(FlightSearch searchFlight)
        {
            var filteredFlights = _context.Flights.Where(flight =>
                flight.From.AirportCode.Trim().ToLower() == searchFlight.From.Trim().ToLower() &&
                flight.To.AirportCode.Trim().ToLower() == searchFlight.To.Trim().ToLower() &&
                flight.DepartureTime.Substring(0, 10) == searchFlight.DepartureDate).ToList();

            var result = new PageResult()
            {
                Page = filteredFlights.Count > 1 ? 1 : 0,
                TotalItems = filteredFlights.Count,
                Items = new List<Flight>(filteredFlights)
            };

            return result;
        }
    }
}
