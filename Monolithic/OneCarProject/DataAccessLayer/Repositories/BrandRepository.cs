using LinqToDB;
using OneCarProject.DataAccessLayer.Entities;
using OneCarProject.DataAccessLayer.Interfaces;
using System.Threading.Tasks;

namespace OneCarProject.DataAccessLayer.Repositories
{
    public class BrandRepository : BaseRepository<BrandEntity, int>, IBrandRepository
    {
        public override async Task<BrandEntity> GetAsync(int identity)
        {
            return await GetSingleOrDefaultAsync(u => u.BrandId == identity);
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = new OneCarDbConnection(connectionString))
            {
                await db.GetTable<BrandEntity>().DeleteAsync(x => x.BrandId == id);
            }
        }

        public async Task<bool> CheckBrandExists(int identity)
        {
            return (await GetSingleOrDefaultAsync(u => u.BrandId == identity))?.BrandId > 0;
        }
    }
}
