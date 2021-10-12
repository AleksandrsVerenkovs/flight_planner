using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService: IEntityService<Flight>
    {
        Flight GetFlightById(int id);
        bool Exists(Flight flight);
    }
}
