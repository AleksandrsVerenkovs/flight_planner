using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validation
{
    public class AirportCodeValidator : IValidator
    {
        public bool IsValid(FlightRequest request)
        {
            return !string.IsNullOrEmpty(request?.To?.Airport) && !string.IsNullOrEmpty(request?.From?.Airport);
        }
    }
}
