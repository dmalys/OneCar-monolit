using Newtonsoft.Json;

namespace OneCarProject.BusinessLayer.CarModel.Models
{
    public class GetCarModelResponse
    {
        [JsonProperty("carModel")]
        public CarModelDTO CarModel { get; set; }
    }
}
