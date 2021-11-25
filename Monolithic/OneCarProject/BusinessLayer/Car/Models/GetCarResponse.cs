using Newtonsoft.Json;

namespace OneCarProject.BusinessLayer.Car.Models
{
    public class GetCarResponse
    {
        [JsonProperty("car")]
        public CarDTO Car { get; set; }
    }
}
