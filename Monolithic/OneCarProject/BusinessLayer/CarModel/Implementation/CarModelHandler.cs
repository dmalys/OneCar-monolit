using OneCarProject.BusinessLayer.CarImage.Interfaces;
using OneCarProject.BusinessLayer.CarModel.Models;
using OneCarProject.BusinessLayer.ErrorHandling;
using OneCarProject.DataAccessLayer.Entities;
using OneCarProject.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneCarProject.BusinessLayer.CarImage.Implementation
{
    public class CarModelHandler : ICarModelHandler
    {
        private readonly ICarModelsRepository _carModelRepository;
        private readonly IBrandRepository _brandRepository;

        public CarModelHandler(ICarModelsRepository carModelRepository,
            IBrandRepository brandRepository)
        {
            _carModelRepository = carModelRepository;
            _brandRepository = brandRepository;
        }

        public async Task AddCarModel(AddCarModelRequest request)
        {
            ValidateRequest(request);

            var carModelEntity = new CarModelEntity();
            carModelEntity.CarModelName = request.CarModelName;
            carModelEntity.CreateDate = DateTime.UtcNow;
            carModelEntity.CreatedBy = request.CreatedBy;//userName;
            carModelEntity.BrandId = request.BrandId;
            
            try
            {
                var brandExists = await _brandRepository.CheckBrandExists(request.BrandId);

                if (!brandExists)
                {
                    throw new SystemBaseException("Brand do not exist.", SystemErrorCode.ValidationError);
                }

                await _carModelRepository.Insert(carModelEntity);
            }
            catch (SystemBaseException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new SystemBaseException("AddCarModel failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task DeleteCarModel(DeleteCarModelRequest request)
        {
            ValidateRequest(request);

            try
            {
                if (await _carModelRepository.CheckCarModelExists(request.CarModelId))
                {
                    await _carModelRepository.DeleteAsync(request.CarModelId);
                }
            }
            catch (Exception)
            {
                throw new SystemBaseException("DeleteCarModel failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task<GetCarModelResponse> GetCarModel(GetCarModelRequest request)
        {
            ValidateRequest(request);

            try
            {
                var carEntity = await _carModelRepository.GetAsync(request.CarModelId);

                if(carEntity == null)
                {
                    return new GetCarModelResponse();
                }

                return new GetCarModelResponse
                {
                    CarModel = ConvertFromEntity(carEntity)
                };
            }
            catch (Exception)
            {
                throw new SystemBaseException("GetCarModel failed.", SystemErrorCode.SystemError);
            }
            
        }

        public async Task<GetCarModelsResponse> GetCarModels()
        {
            try
            {
                var allEntities = await _carModelRepository.GetAll();

                if(allEntities.Count == 0)
                {
                    return new GetCarModelsResponse { CarModelList = new List<CarModelDTO>() };
                }

                return ConvertEntitiesToResponse(allEntities);
            }
            catch (Exception)
            {
                throw new SystemBaseException("GetCarModels failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task UpdateCarModel(UpdateCarModelRequest request)
        {
            ValidateRequest(request);
            if (request.CarModelId == 0)
            {
                throw new SystemBaseException("CarModelId is not valid.", SystemErrorCode.ValidationError);
            }

            var carModelEntity = new CarModelEntity();
            carModelEntity.CarModelName = request.CarModelName;
            carModelEntity.UpdateDate = DateTime.UtcNow;
            carModelEntity.UpdatedBy = request.UpdatedBy;
            carModelEntity.BrandId = request.BrandId;

            try
            {
                var brandExists = await _brandRepository.CheckBrandExists(request.BrandId);

                if (!brandExists)
                {
                    throw new SystemBaseException("Brand do not exist.", SystemErrorCode.EntityNotFound);
                }

                if (await _carModelRepository.CheckCarModelExists(request.CarModelId))
                {
                    await _carModelRepository.Update(carModelEntity);
                }
                else
                {
                    throw new SystemBaseException("Entity not found", SystemErrorCode.EntityNotFound);
                }
            }
            catch (Exception)
            {
                throw new SystemBaseException("UpdateCarModel failed.", SystemErrorCode.SystemError);
            }
        }

        private GetCarModelsResponse ConvertEntitiesToResponse(List<CarModelEntity> allentities)
        {
            var entities = allentities.Select(x => ConvertFromEntity(x)).ToList();
            return new GetCarModelsResponse
            {
                CarModelList = entities
            };
        }

        private CarModelDTO ConvertFromEntity(CarModelEntity x)
        {
            return new CarModelDTO
            {
                CarModelId = x.CarModelId,
                CarModelName = x.CarModelName,
                CreateDate = x.CreateDate,
                CreatedBy = x.CreatedBy,
                UpdateDate = x.UpdateDate,
                UpdatedBy = x.UpdatedBy,
                BrandId = x.BrandId
            };
        }

        private void ValidateRequest(CarModelIdRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (request.CarModelId == 0)
            {
                throw new SystemBaseException("CarModelId is not valid.", SystemErrorCode.ValidationError);
            }
        }

        private void ValidateRequest(CarModelDetailedRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (request.BrandId == 0)
            {
                throw new SystemBaseException("BrandId is not valid.", SystemErrorCode.ValidationError);
            }

            if (string.IsNullOrWhiteSpace(request.CarModelName))
            {
                throw new SystemBaseException("CarModelName is not valid.", SystemErrorCode.ValidationError);
            }
        }
    }
}
