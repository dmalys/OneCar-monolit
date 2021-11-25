using Newtonsoft.Json;
using OneCarProject.BusinessLayer.User.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneCarProject.BusinessLayer.Coupon.Models
{
    public class GetCouponsResponse
    {
        [JsonProperty("couponList")]
        public List<CouponDTO> CouponList { get; set; }
    }
}
