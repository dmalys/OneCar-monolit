using Newtonsoft.Json;

namespace OneCarProject.BusinessLayer.Brand.Models
{
    public class GetBrandResponse
    {
        [JsonProperty("brand")]
        public BrandDTO Brand { get; set; }
    }
}
