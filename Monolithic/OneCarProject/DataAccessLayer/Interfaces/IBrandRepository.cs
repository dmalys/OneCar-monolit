using OneCarProject.DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneCarProject.DataAccessLayer.Interfaces
{
    public interface IBrandRepository
    {
        Task<BrandEntity> GetAsync(int identity);
        Task<int> Insert(BrandEntity brand);
        Task<int> Update(BrandEntity brand);

        Task<List<BrandEntity>> GetAll();

        Task DeleteAsync(int id);
        Task<bool> CheckBrandExists(int identity);

    }
}
