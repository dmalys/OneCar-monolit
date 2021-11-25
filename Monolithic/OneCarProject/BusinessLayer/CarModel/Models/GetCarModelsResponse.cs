using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneCarProject.BusinessLayer.CarModel.Models
{
    public class GetCarModelsResponse
    {
        [JsonProperty("carModelList")]
        public IList<CarModelDTO> CarModelList { get; set; }
    }
}
