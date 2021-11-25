using LinqToDB;
using OneCarProject.DataAccessLayer.Entities;
using OneCarProject.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OneCarProject.DataAccessLayer
{
    public abstract class BaseRepository<T, I> : IBaseRepository<T, I>
            where T : class, IBaseEntity
    {
        public string connectionString = ConnectionString.CName;

        protected OneCarDbConnection CreateConnection()
        {
            return new OneCarDbConnection(connectionString);
        }
        
        public async Task<I> Insert(T entity)
        {
            using (var db = new OneCarDbConnection(connectionString))
            {
                var identity = await db.InsertWithIdentityAsync(entity);
                return (I)Convert.ChangeType(identity, typeof(I));
            }
        }

        public async Task<int> Update(T entity)
        {
            using (var db = new OneCarDbConnection(connectionString))
            {
                return await db.UpdateAsync(entity);
            }
        }

        public async Task<int> CountByCriteria(Expression<Func<T, bool>> where)
        {
            using (var db = new OneCarDbConnection(connectionString))
            {
                return await db.GetTable<T>().CountAsync(where);
            }
        }

        public async Task<int> Delete(T entity)
        {
            using (var db = new OneCarDbConnection(connectionString))
            {
                return await db.DeleteAsync(entity);
            }
        }

        public abstract Task<T> GetAsync(I identity);

        public virtual async Task<T> GetSingleOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            using (var db = new OneCarDbConnection(connectionString))
            {
                var table = db.GetTable<T>();
                return await table.Where(where).SingleOrDefaultAsync();
            }
        }

        public virtual async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> where)
        {
            using (var db = new OneCarDbConnection(connectionString))
            {
                var table = db.GetTable<T>();
                return await table.Where(where).FirstOrDefaultAsync();
            }
        }

        public async Task<List<T>> GetAll()
        {
            using (var db = new OneCarDbConnection(connectionString))
            {
                return await db.GetTable<T>().ToListAsync();
            }
        }
    }
}
