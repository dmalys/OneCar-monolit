using OneCarProject.BusinessLayer.Coupon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneCarProject.BusinessLayer.Coupon.Interfaces
{
    public interface ICouponHandler
    {
        Task<GetCouponResponse> GetCoupon(GetCouponRequest request);

        Task DeleteCoupon(DeleteCouponRequest request);

        Task AddCoupon(AddCouponRequest request);

        Task UpdateCoupon(UpdateCouponRequest request);

        Task<GetCouponsResponse> GetCoupons(); //Only for admins
    }
}
