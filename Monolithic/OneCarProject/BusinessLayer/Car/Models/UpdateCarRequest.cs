namespace OneCarProject.BusinessLayer.Car.Models
{
    public class UpdateCarRequest : CarDetailedRequest
    {
        public string UpdatedBy { get; set; }
        public int CarId { get; set; }
    }
}
