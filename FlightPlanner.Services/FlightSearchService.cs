using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using System.Linq;

namespace FlightPlanner.Services
{
    public class FlightSearchService : EntityService<Airport>, IFlightSearchService
    {
        public FlightSearchService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Airport GetByTag(string tag)
        {
            var str = tag.Trim().ToLower();
            var airport = _context.Airports.SingleOrDefault(f => f.AirportCode.ToLower().Contains(str) || f.City.ToLower().Contains(str) || f.Country.ToLower().Contains(str));
            return airport;
        }
    }
}
