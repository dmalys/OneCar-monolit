using LinqToDB;
using OneCarProject.DataAccessLayer.Entities;
using OneCarProject.DataAccessLayer.Interfaces;
using System.Threading.Tasks;

namespace OneCarProject.DataAccessLayer.Repository
{
    public class CarRepository : BaseRepository<CarEntity, int>, ICarRepository
    {
        public override async Task<CarEntity> GetAsync(int identity)
        {
            return await GetSingleOrDefaultAsync(u => u.CarId == identity);
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = new OneCarDbConnection(connectionString))
            {
                await db.GetTable<CarEntity>().DeleteAsync(x => x.CarId == id);
            }
        }

        public async Task<bool> CheckCarExists(int identity)
        {
            return (await GetSingleOrDefaultAsync(u => u.CarId == identity))?.CarId > 0;
        }
    }
}
