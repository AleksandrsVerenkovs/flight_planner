using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IPageResultService : IEntityService<Flight>
    {
        PageResult GetPageResult(FlightSearch flight);
    }
}
