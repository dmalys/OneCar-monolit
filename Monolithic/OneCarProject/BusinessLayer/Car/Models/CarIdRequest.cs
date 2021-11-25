﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneCarProject.BusinessLayer.Car.Models
{
    public abstract class CarIdRequest
    {
        [JsonProperty("carId")]
        public int CarId { get; set; }
    }
}
