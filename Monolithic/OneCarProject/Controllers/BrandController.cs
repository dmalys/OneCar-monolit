using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneCarProject.BusinessLayer.Brand.Implementation;
using OneCarProject.BusinessLayer.Brand.Models;
using Swashbuckle.Swagger.Annotations;

namespace OneCarProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ILogger<BrandController> _logger;
        private readonly IBrandHandler _brandHandler;

        public BrandController(ILogger<BrandController> logger,
             IBrandHandler brandHandler)
        {
            _logger = logger;
            _brandHandler = brandHandler;
        }

        [HttpPost]
        [Route("GetBrands")]
        [SwaggerOperation(OperationId = "GetBrands")]
        [ProducesResponseType(typeof(GetBrandsResponse), StatusCodes.Status200OK)]
        public async Task<GetBrandsResponse> GetBrands()
        {
            return await _brandHandler.GetBrands();

        }

        [HttpPost]
        [Route("GetDetails")]
        [SwaggerOperation(OperationId = "GetDetails")]
        [ProducesResponseType(typeof(GetBrandResponse), StatusCodes.Status200OK)]
        public async Task<GetBrandResponse> GetDetails([FromBody] GetBrandRequest request)
        {
            return await _brandHandler.GetBrand(request);
        }

        [HttpPost]
        [Route("UpdateBrand")]
        [SwaggerOperation(OperationId = "UpdateBrand")]
        public async Task UpdateBrand([FromBody]UpdateBrandRequest request)
        {
            await _brandHandler.UpdateBrand(request);
        }

        [HttpPost]
        [Route("AddBrand")]
        [SwaggerOperation(OperationId = "AddBrand")]
        public async Task AddCar([FromBody]AddBrandRequest request)
        {
            await _brandHandler.AddBrand(request);
        }

        [HttpPost]
        [Route("DeleteBrand")]
        [SwaggerOperation(OperationId = "DeleteBrand")]
        public async Task DeleteBrand([FromBody]DeleteBrandRequest request)
        {
            await _brandHandler.DeleteBrand(request);
        }
    }
}
