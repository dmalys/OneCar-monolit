using LinqToDB;
using OneCarProject.DataAccessLayer.Entities;
using OneCarProject.DataAccessLayer.Interfaces;
using System.Threading.Tasks;

namespace OneCarProject.DataAccessLayer.Repositories
{
    public class CarImageRepository : BaseRepository<CarImageEntity, int>, ICarImageRepository
    {
        public override async Task<CarImageEntity> GetAsync(int identity)
        {
            return await GetSingleOrDefaultAsync(u => u.CarImageId == identity);
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = new OneCarDbConnection(connectionString))
            {
                await db.GetTable<CarImageEntity>().DeleteAsync(x => x.CarImageId == id);
            }
        }

        public async Task<bool> CheckCarImageExists(int identity)
        {
            return (await GetSingleOrDefaultAsync(u => u.CarImageId == identity))?.CarImageId > 0;
        }
    }
}