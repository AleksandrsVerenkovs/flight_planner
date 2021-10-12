using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using System;

namespace FlightPlanner.Core.Services
{
    public interface IFlightSearchService : IEntityService<Airport>
    {
        Airport GetByTag(string tag);
    }
}
