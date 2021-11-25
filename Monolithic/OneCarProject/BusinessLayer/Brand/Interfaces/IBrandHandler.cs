using OneCarProject.BusinessLayer.Brand.Models;
using System.Threading.Tasks;

namespace OneCarProject.BusinessLayer.Brand.Implementation
{
    public interface IBrandHandler
    {
        Task<GetBrandResponse> GetBrand(GetBrandRequest request);

        Task UpdateBrand(UpdateBrandRequest request);

        Task DeleteBrand(DeleteBrandRequest request);

        Task AddBrand(AddBrandRequest request);

        Task<GetBrandsResponse> GetBrands();
    }
}
