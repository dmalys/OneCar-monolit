using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneCarProject.BusinessLayer.Brand.Models
{
    public abstract class BrandIdRequest
    {
        [JsonProperty("brandId")]
        public int BrandId { get; set; }
    }
}
