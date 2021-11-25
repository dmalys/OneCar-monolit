namespace OneCarProject.BusinessLayer.Coupon.Models
{
    public class UpdateCouponRequest : CouponDetailedRequest
    {
        public string UpdatedBy { get; set; }
        public int CouponId { get; set; }
    }
}
