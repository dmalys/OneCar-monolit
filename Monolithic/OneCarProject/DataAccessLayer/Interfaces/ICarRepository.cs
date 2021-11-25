using OneCarProject.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneCarProject.DataAccessLayer.Interfaces
{
    public interface ICarRepository
    {
        Task<CarEntity> GetAsync(int identity);
        Task<int> Insert(CarEntity car);
        Task<int> Update(CarEntity car);

        Task<List<CarEntity>> GetAll();

        Task DeleteAsync(int id);
        Task<bool> CheckCarExists(int identity);

    }
}
