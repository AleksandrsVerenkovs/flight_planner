using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validation.FlightSearch
{
    public class SearchAirportValidator : ISearchValidator
    {
        public bool IsValid(Core.Models.FlightSearch flight)
        {
            var result = true;
            if (string.IsNullOrEmpty(flight.From) || string.IsNullOrEmpty(flight.To) || string.IsNullOrEmpty(flight.DepartureDate))
            {
                result = false;
            }

            return result;
        }
    }
}
