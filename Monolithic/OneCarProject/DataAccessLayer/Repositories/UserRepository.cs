using LinqToDB;
using OneCarProject.DataAccessLayer.Entities;
using OneCarProject.DataAccessLayer.Interfaces;
using System.Threading.Tasks;

namespace OneCarProject.DataAccessLayer.Repositories
{
    public class UserRepository : BaseRepository<UserEntity, int>, IUserRepository
    {
        public override async Task<UserEntity> GetAsync(int identity)
        {
            return await GetSingleOrDefaultAsync(u => u.UserId == identity);
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = new OneCarDbConnection(connectionString))
            {
                await db.GetTable<UserEntity>().DeleteAsync(x => x.UserId == id);
            }
        }

        public async Task<bool> CheckUserExists(int identity)
        {
            return (await GetSingleOrDefaultAsync(u => u.UserId == identity))?.UserId > 0;
        }
    }
}
