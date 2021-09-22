using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Flight_Planner.DbContext;
using Flight_Planner.Models;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Microsoft.EntityFrameworkCore;

namespace Flight_Planner.Repository
{
    public class FlightList
    {
        private readonly FlightPlannerDbContext _context = null;

        public FlightList(FlightPlannerDbContext context)
        {
            _context = context;
        }

        public Flight GetById(int id)
        {
            return _context.Flights
                .Include(f => f.To)
                .Include(f => f.From)
                .SingleOrDefault(flight => flight.Id == id);
        }

        public void ClearFlight()
        {
            _context.Flights.RemoveRange(_context.Flights);
            _context.Airports.RemoveRange(_context.Airports);
            _context.SaveChanges();
        }

        public Flight AddFlight(Flight flight)
        {
            _context.Flights.Add(flight);
            _context.SaveChanges();
            return flight;
        }

        public bool Exists(Flight flight)
        {
            return _context.Flights.Any(f =>
            f.From.AirportCode == flight.From.AirportCode &&
            f.To.AirportCode == flight.To.AirportCode &&
            f.Carrier == flight.Carrier &&
            f.DepartureTime == flight.DepartureTime &&
            f.ArrivalTime == flight.ArrivalTime);
        }

        public bool IsValid(Flight flight)
        {
            if (flight.From == null)
                return false;
            if (flight.To == null)
                return false;
            if (string.IsNullOrEmpty(flight.To.City) || string.IsNullOrEmpty(flight.To.Country) || string.IsNullOrEmpty(flight.To.AirportCode))
                return false;
            if (string.IsNullOrEmpty(flight.From.City) || string.IsNullOrEmpty(flight.From.Country) || string.IsNullOrEmpty(flight.From.AirportCode))
                return false;
            if (string.IsNullOrEmpty(flight.Carrier) || string.IsNullOrEmpty(flight.DepartureTime) || string.IsNullOrEmpty(flight.ArrivalTime))
                return false;
            if (flight.To.AirportCode.Trim().ToLower() == flight.From.AirportCode.Trim().ToLower())
                return false;
            if (DateTime.Parse(flight.ArrivalTime) <= DateTime.Parse(flight.DepartureTime))
                return false;
            return true;
        }
        
        public bool IsValidFlight(FlightSearch flight)
        {
            var result = true;
            if (string.IsNullOrEmpty(flight.From) || string.IsNullOrEmpty(flight.To) || string.IsNullOrEmpty(flight.DepartureDate))
            {
                result = false;
            }
            
            return result;
        }

        public bool IsSameAirport(FlightSearch flight)
        {
            return flight.From != flight.To;
        }

        public PageResult SearchResult(FlightSearch searchItem)
        {
            var filteredFlights = _context.Flights.Include(f => f.To).Include(f => f.From).Where(flight =>
                flight.From.AirportCode.Trim().ToLower() == searchItem.From.Trim().ToLower() &&
                flight.To.AirportCode.Trim().ToLower() == searchItem.To.Trim().ToLower() &&
                flight.DepartureTime.Substring(0,10) == searchItem.DepartureDate).ToList();

            var result = new PageResult()
            {
                Page = filteredFlights.Count > 1 ? 1 : 0,
                TotalItems = filteredFlights.Count,
                Items = new List<Flight>(filteredFlights)
            };

            return result;
        }

        public void DeleteFlight(int id)
        {
            var item = _context.Flights.Include(f => f.To).Include(f => f.From).SingleOrDefault(f => f.Id == id);
            if(item != null)
            {
                _context.Airports.Remove(item.To);
                _context.Airports.Remove(item.From);
                _context.Flights.Remove(item);
                _context.SaveChanges();
            }
        }

        public Airport[] GetByTag(string search)
        {
            var str = search.Trim().ToLower();
            var airport = _context.Airports.SingleOrDefault(f => f.AirportCode.ToLower().Contains(str) || f.City.ToLower().Contains(str) || f.Country.ToLower().Contains(str));
            return new []{ airport};
        }

    }
}
