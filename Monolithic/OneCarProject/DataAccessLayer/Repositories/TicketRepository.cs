using LinqToDB;
using OneCarProject.DataAccessLayer.Entities;
using OneCarProject.DataAccessLayer.Interfaces;
using System.Threading.Tasks;

namespace OneCarProject.DataAccessLayer.Repositories
{
    public class TicketRepository : BaseRepository<TicketEntity, int>, ITicketRepository
    {
        public override async Task<TicketEntity> GetAsync(int identity)
        {
            return await GetSingleOrDefaultAsync(u => u.TicketId == identity);
        }

        public async Task DeleteAsync(int id)
        {
            using (var db = new OneCarDbConnection(connectionString))
            {
                await db.GetTable<TicketEntity>().DeleteAsync(x => x.TicketId == id);
            }
        }

        public async Task<bool> CheckTicketExists(int identity)
        {
            return (await GetSingleOrDefaultAsync(u => u.TicketId == identity))?.TicketId > 0;
        }
    }
}
