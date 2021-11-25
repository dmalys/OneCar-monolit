using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneCarProject.BusinessLayer.Brand.Models
{
    public class GetBrandsResponse
    {
        [JsonProperty("brandList")]
        public IList<BrandDTO> BrandList { get; set; }
    }
}
