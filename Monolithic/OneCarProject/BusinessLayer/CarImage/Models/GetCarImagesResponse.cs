using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneCarProject.BusinessLayer.CarImage.Models
{
    public class GetCarImagesResponse
    {
        [JsonProperty("carImageList")]
        public IList<CarImageDTO> CarImageList { get; set; }
    }
}
