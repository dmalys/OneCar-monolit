using OneCarProject.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneCarProject.DataAccessLayer.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> GetAsync(int identity);
        Task<int> Insert(UserEntity user);
        Task<int> Update(UserEntity user);

        Task<List<UserEntity>> GetAll();

        Task DeleteAsync(int id);
        Task<bool> CheckUserExists(int identity);

    }
}
