namespace OneCarProject.BusinessLayer.CarModel.Models
{
    public class UpdateCarModelRequest : CarModelDetailedRequest
    {        
        public string UpdatedBy { get; set; }
        public int CarModelId { get; set; }
    }
}
