using LinqToDB;
using OneCarProject.DataAccessLayer.Entities;
using OneCarProject.DataAccessLayer.Interfaces;
using System.Threading.Tasks;

namespace OneCarProject.DataAccessLayer.Repositories
{
    public class CarModelsRepository : BaseRepository<CarModelEntity, int>, ICarModelsRepository
    {
        public override async Task<CarModelEntity> GetAsync(int identity)
        {
            return await GetSingleOrDefaultAsync(u => u.CarModelId == identity);
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = new OneCarDbConnection(connectionString))
            {
                await db.GetTable<CarModelEntity>().DeleteAsync(x => x.CarModelId == id);
            }
        }

        public async Task<bool> CheckCarModelExists(int identity)
        {
            return (await GetSingleOrDefaultAsync(u => u.CarModelId == identity))?.CarModelId > 0;
        }
    }
}
