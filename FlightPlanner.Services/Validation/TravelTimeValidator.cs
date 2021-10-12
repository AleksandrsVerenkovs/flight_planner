using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;
using System;

namespace FlightPlanner.Services.Validation
{
    public class TravelTimeValidator : IValidator
    {
        public bool IsValid(FlightRequest request)
        {
            try
            {
                return DateTime.Parse(request.ArrivalTime) > DateTime.Parse(request.DepartureTime);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
