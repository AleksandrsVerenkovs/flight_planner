using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        //why?

        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }
        public Flight GetFlightById(int id)
        {
            return _context.Flights.Include(f => f.To).Include(f => f.From).SingleOrDefault(f => f.Id == id);
        }

        public bool Exists(Flight flight)
        {
            return Query().Any(f =>
            f.From.AirportCode == flight.From.AirportCode &&
            f.To.AirportCode == flight.To.AirportCode &&
            f.Carrier == flight.Carrier &&
            f.DepartureTime == flight.DepartureTime &&
            f.ArrivalTime == flight.ArrivalTime);
        }
    }
}
