using Newtonsoft.Json;

namespace OneCarProject.BusinessLayer.CarImage.Models
{
    public class GetCarImageResponse
    {
        [JsonProperty("carImage")]
        public CarImageDTO CarImage { get; set; }
    }
}
