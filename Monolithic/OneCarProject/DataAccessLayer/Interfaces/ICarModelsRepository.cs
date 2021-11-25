using OneCarProject.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneCarProject.DataAccessLayer.Interfaces
{
    public interface ICarModelsRepository
    {
        Task<CarModelEntity> GetAsync(int identity);
        Task<int> Insert(CarModelEntity carModel);
        Task<int> Update(CarModelEntity carModel);

        Task<List<CarModelEntity>> GetAll();

        Task DeleteAsync(int id);
        Task<bool> CheckCarModelExists(int identity);

    }
}
