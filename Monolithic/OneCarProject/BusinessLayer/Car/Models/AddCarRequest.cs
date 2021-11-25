using System;

namespace OneCarProject.BusinessLayer.Car.Models
{
    public class AddCarRequest : CarDetailedRequest
    {
        public string CreatedBy { get; set; }
    }
}
