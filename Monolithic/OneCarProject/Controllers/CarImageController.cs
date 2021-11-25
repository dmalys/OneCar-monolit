using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneCarProject.BusinessLayer.CarImage.Interfaces;
using OneCarProject.BusinessLayer.CarImage.Models;
using Swashbuckle.Swagger.Annotations;

namespace OneCarProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImageController : ControllerBase
    {
        private readonly ILogger<CarImageController> _logger;
        private readonly ICarImageHandler _carImageHandler;

        public CarImageController(ILogger<CarImageController> logger,
             ICarImageHandler carImageHandler)
        {
            _logger = logger;
            _carImageHandler = carImageHandler;
        }

        [HttpPost]
        [Route("GetCarImages")]
        [SwaggerOperation(OperationId = "GetCarImages")]
        [ProducesResponseType(typeof(GetCarImagesResponse), StatusCodes.Status200OK)]
        public async Task<GetCarImagesResponse> GetCarImages()
        {
            return await _carImageHandler.GetCarImages();
        }

        [HttpPost]
        [Route("GetDetails")]
        [SwaggerOperation(OperationId = "GetDetails")]
        [ProducesResponseType(typeof(GetCarImageResponse), StatusCodes.Status200OK)]
        public async Task<GetCarImageResponse> GetDetails([FromBody] GetCarImageRequest request)
        {
            return await _carImageHandler.GetCarImage(request);
        }

        [HttpPost]
        [Route("UpdateCarImage")]
        [SwaggerOperation(OperationId = "UpdateCarImage")]
        public async Task UpdateCarImage([FromBody]UpdateCarImageRequest request)
        {
            await _carImageHandler.UpdateCarImage(request);
        }

        [HttpPost]
        [Route("AddCarImage")]
        [SwaggerOperation(OperationId = "AddCarImage")]
        public async Task AddCar([FromForm]AddCarImageRequest request)
        {
            await _carImageHandler.AddCarImage(request);//filename, uploadedBy, file);
        }

        [HttpPost]
        [Route("DeleteCarImage")]
        [SwaggerOperation(OperationId = "DeleteCarImage")]
        public async Task DeleteCarImage([FromBody]DeleteCarImageRequest request)
        {
            await _carImageHandler.DeleteCarImage(request);
        }
    }
}
