using OneCarProject.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneCarProject.DataAccessLayer.Interfaces
{
    public interface ICouponRepository
    {
        Task<CouponEntity> GetAsync(int identity);
        Task<int> Insert(CouponEntity coupon);
        Task<int> Update(CouponEntity coupon);

        Task<List<CouponEntity>> GetAll();

        Task DeleteAsync(int id);


        Task<CouponEntity> GetAsyncByCode(string couponCode);
        Task<bool> CheckCouponExists(int identity);

    }
}
