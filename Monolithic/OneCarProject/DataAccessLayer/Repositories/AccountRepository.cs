using LinqToDB;
using OneCarProject.DataAccessLayer.Entities;
using OneCarProject.DataAccessLayer.Interfaces;
using System.Threading.Tasks;

namespace OneCarProject.DataAccessLayer.Repositories
{
    public class AccountRepository : BaseRepository<AccountEntity, int>, IAccountRepository
    {
        public override async Task<AccountEntity> GetAsync(int identity)
        {
            return await GetSingleOrDefaultAsync(u => u.AccountId == identity);
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = new OneCarDbConnection(connectionString))
            {
                await db.GetTable<AccountEntity>().DeleteAsync(x => x.AccountId == id);
            }
        }

        public async Task<bool> CheckAccountExists(int identity)
        {
            return (await GetSingleOrDefaultAsync(u => u.AccountId == identity))?.AccountId > 0;
        }
    }
}
