using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Flight_Planner.Models;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;

namespace Flight_Planner.Repository
{
    public static class FlightList
    {
        private static List<Flight> _flights = new List<Flight>();
        private static int _count = 1;

        public static Flight GetById(int id)
        {
            return _flights.SingleOrDefault(flight => flight.Id == id);
        }

        public static void ClearFlight()
        {
            _flights.Clear();
        }

        public static Flight AddFlight(Flight flight)
        {
                flight.Id = _count;
                _count++;
                _flights.Add(flight);
                return flight;
        }

        public static bool Exists(Flight flight)
        {
            return _flights.Any(f =>
            f.From.AirportCode == flight.From.AirportCode &&
            f.To.AirportCode == flight.To.AirportCode &&
            f.Carrier == flight.Carrier &&
            f.DepartureTime == flight.DepartureTime &&
            f.ArrivalTime == flight.ArrivalTime);
        }

        public static bool IsValid(Flight flight)
        {
            //var result = true;
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

        
        public static bool IsValidFlight(FlightSearch flight)
        {
            var result = true;
            if (string.IsNullOrEmpty(flight.From) || string.IsNullOrEmpty(flight.To) || string.IsNullOrEmpty(flight.DepartureDate))
                {
                    result = false;
                }
            
            return result;
        }

        public static bool IsSameAirport(FlightSearch flight)
        {
            if (flight.From == flight.To)
                return false;
            return true;
        }

        public static PageResult SearchResult(FlightSearch searchItem)
        {
            var fliteredFlights = _flights.Where(flight =>
                flight.From.AirportCode.Trim().ToLower() == searchItem.From.Trim().ToLower() &&
                flight.To.AirportCode.Trim().ToLower() == searchItem.To.Trim().ToLower() &&
                flight.DepartureTime.Substring(0,10) == searchItem.DepartureDate).ToList();

            var result = new PageResult()
            {
                Page = fliteredFlights.Count > 1 ? 1 : 0,
                TotalItems = fliteredFlights.Count,
                Items = new List<Flight>(fliteredFlights)
            };

            return result;
        }
        

        public static void DeleteFlight(int id)
        {
                var item = _flights.SingleOrDefault(f => f.Id == id);
                _flights.Remove(item);
        }

        public static Airport[] GetByTag(string search)
        {
            var str = search.Trim().ToLower();
            var airport = _flights.SingleOrDefault(f => f.From.AirportCode.ToLower().Contains(str) || f.From.City.ToLower().Contains(str) || f.From.Country.ToLower().Contains(str)).From;
            return new[] { airport };
        }

    }
}
