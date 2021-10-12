using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validation
{
    public class AirportConnectValidator : IValidator
    {
        public bool IsValid(FlightRequest request)
        {
            return request?.To?.Airport?.Trim().ToLower() != request?.From?.Airport?.Trim().ToLower();
        }
    }
}
