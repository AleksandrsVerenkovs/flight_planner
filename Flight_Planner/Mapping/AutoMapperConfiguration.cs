using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;

namespace Flight_Planner.Mapping
{
    public class AutoMapperConfiguration
    {
        public static IMapper GetConfig()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.CreateMap<FlightRequest,Flight>().ForMember(
                    f => f.Id, opt => opt.Ignore());
                cfg.CreateMap<AirportRequest, Airport>().ForMember(f => f.AirportCode, opt => opt.MapFrom(s=> s.Airport)).ForMember(f => f.Id, opt => opt.Ignore());
                cfg.CreateMap<Flight, FlightResponse>();
                cfg.CreateMap<Airport, AirportResponse>().ForMember(f => f.Airport, opt => opt.MapFrom(s => s.AirportCode));
            });

            configuration.AssertConfigurationIsValid();
            var mapper = configuration.CreateMapper();
            return mapper;
        }
    }
}
