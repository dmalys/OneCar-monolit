using LinqToDB;
using OneCarProject.DataAccessLayer.Entities;
using OneCarProject.DataAccessLayer.Interfaces;
using System.Threading.Tasks;

namespace OneCarProject.DataAccessLayer.Repositories
{
    public class CouponRepository : BaseRepository<CouponEntity, int>, ICouponRepository
    {
        public override async Task<CouponEntity> GetAsync(int identity)
        {
            return await GetSingleOrDefaultAsync(u => u.CouponId == identity);
        }

        public async Task<CouponEntity> GetAsyncByCode(string couponCode)
        {
            return await GetSingleOrDefaultAsync(u => u.Code == couponCode);
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = new OneCarDbConnection(connectionString))
            {
                await db.GetTable<CouponEntity>().DeleteAsync(x => x.CouponId == id);
            }
        }

        public async Task<bool> CheckCouponExists(int identity)
        {
            return (await GetSingleOrDefaultAsync(u => u.CouponId == identity))?.CouponId > 0;
        }
    }
}
