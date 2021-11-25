using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OneCarProject.BusinessLayer.CarImage.Models
{
    public class AddCarImageRequest : CarImageDetailedRequest
    {
        public string CreatedBy { get; set; }
    }
}
