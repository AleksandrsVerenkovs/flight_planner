
namespace FlightPlanner.Core.Models
{
    public class Airport: Entity
    {
        //[JsonIgnore]
        public string Country { get; set; }
        public string City { get; set; }
        //[JsonPropertyName("airport")]
        public string AirportCode { get; set; }
    }
}
