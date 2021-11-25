using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneCarProject.BusinessLayer.Car.Models
{
    public class GetCarsResponse
    {
        [JsonProperty("carList")]
        public List<CarDTO> CarList { get; set; }
    }
}
