using OneCarProject.BusinessLayer.Car.Interfaces;
using OneCarProject.BusinessLayer.Car.Models;
using OneCarProject.BusinessLayer.ErrorHandling;
using OneCarProject.DataAccessLayer.Entities;
using OneCarProject.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneCarProject.BusinessLayer.Car.Implementation
{
    public class CarHandler : ICarHandler
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarImageRepository _carImageRepository;
        private readonly ICarModelsRepository _carModelRepository;

        public CarHandler(
            ICarRepository carRepository,
            ICarImageRepository carImageRepository,
            ICarModelsRepository carModelsRepository
            )
        {
            _carRepository = carRepository;
            _carImageRepository = carImageRepository;
            _carModelRepository = carModelsRepository;
        }

        public async Task<GetCarsResponse> GetCars()
        {
            try
            {
                var allEntities = await _carRepository.GetAll();

                if(allEntities.Count == 0)
                {
                    return new GetCarsResponse { CarList = new List<CarDTO>() };
                }

                return ConvertEntities(allEntities);
            }
            catch (Exception)
            {
                throw new SystemBaseException("AddCar failed.", SystemErrorCode.SystemError);
            }           
        }

        public async Task AddCar(AddCarRequest request)
        {
            ValidateRequest(request);

            if (request.CarImageId != null && request.CarImageId > 0)
            {
                var imageResult = await _carImageRepository.CheckCarImageExists(request.CarImageId.Value);

                if (!imageResult)
                {
                    throw new SystemBaseException("CarImage entity was not found.", SystemErrorCode.EntityNotFound);
                }
            }

            if (request.CarModelId > 0)
            {
                var modelResult = await _carModelRepository.CheckCarModelExists(request.CarModelId);

                if (!modelResult)
                {
                    throw new SystemBaseException("CarModel entity was not found.", SystemErrorCode.EntityNotFound);
                }
            }

            var carEntity = new CarEntity();
            carEntity.CreateDate = DateTime.UtcNow;
            carEntity.CarImageId = request.CarImageId != null ? request.CarImageId : null;
            carEntity.CarModelId = request.CarModelId;
            carEntity.CreatedBy = request.CreatedBy;//userName;
            carEntity.Localization = request.Localization;
            carEntity.Mileage = request.Mileage;
            carEntity.PricePerHour = request.PricePerHour;
            carEntity.ProductionDate = request.ProductionDate;

            try
            {
                await _carRepository.Insert(carEntity);
            }
            catch (Exception)
            {
                throw new SystemBaseException("AddCar failed.", SystemErrorCode.SystemError);
            }
        }
        
        public async Task DeleteCar(DeleteCarRequest request)
        {
            ValidateRequest(request);

            try
            {
                if (await _carRepository.CheckCarExists(request.CarId))
                {
                    await _carRepository.DeleteAsync(request.CarId);
                }
            }
            catch (Exception)
            {
                throw new SystemBaseException("DeleteCar failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task UpdateCar(UpdateCarRequest request)
        {
            ValidateRequest(request);
            if (request.CarId == 0)
            {
                throw new SystemBaseException("CarId is not valid.", SystemErrorCode.ValidationError);
            }

            if (request.CarImageId != null && request.CarImageId > 0)
            {
                var imageResult = await _carImageRepository.CheckCarImageExists(request.CarImageId.Value);

                if (!imageResult)
                {
                    throw new SystemBaseException("CarImage entity was not found.", SystemErrorCode.EntityNotFound);
                }
            }

            if (request.CarModelId > 0)
            {
                var modelResult = await _carModelRepository.CheckCarModelExists(request.CarModelId);

                if (!modelResult)
                {
                    throw new SystemBaseException("CarModel entity was not found.", SystemErrorCode.EntityNotFound);
                }
            }

            try
            {
                if (await _carRepository.CheckCarExists(request.CarId))
                {
                    var carEntity = await _carRepository.GetAsync(request.CarId);
                    carEntity.UpdateDate = DateTime.UtcNow;
                    carEntity.CarImageId = request.CarImageId != null ? request.CarImageId : null;
                    carEntity.CarModelId = request.CarModelId;
                    carEntity.UpdatedBy = request.UpdatedBy;
                    carEntity.Localization = request.Localization;
                    carEntity.Mileage = request.Mileage;
                    carEntity.PricePerHour = request.PricePerHour;
                    carEntity.ProductionDate = request.ProductionDate;

                    await _carRepository.Update(carEntity);
                }
                else
                {
                    throw new SystemBaseException("Entity not found.", SystemErrorCode.EntityNotFound);
                }
            }
            catch (Exception)
            {
                throw new SystemBaseException("UpdateCar failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task<GetCarResponse> GetCar(GetCarRequest request)
        {
            ValidateRequest(request);

            try
            {
                var carEntity = await _carRepository.GetAsync(request.CarId);

                if(carEntity == null)
                {
                    return new GetCarResponse();
                }

                return new GetCarResponse
                {
                   Car = ConvertFromEntity(carEntity) 
                };
            }
            catch (Exception)
            {
                throw new SystemBaseException("GetCar failed.", SystemErrorCode.SystemError);
            }
        }

        private GetCarsResponse ConvertEntities(List<CarEntity> allentities)
        {
            return new GetCarsResponse
            {
                CarList = allentities.Select(x => ConvertFromEntity(x)).ToList()
            };
        }

        private CarDTO ConvertFromEntity(CarEntity x)
        {
            return  new CarDTO
            {
                 CarId = x.CarId,
                 CarImageId = x.CarImageId,
                 CarModelId = x.CarModelId,
                 CreateDate = x.CreateDate,
                 CreatedBy = x.CreatedBy,
                 Localization = x.Localization,
                 Mileage = x.Mileage,
                 PricePerHour = x.PricePerHour,
                 ProductionDate = x.ProductionDate,
                 UpdateDate = x.UpdateDate,
                 UpdatedBy = x.UpdatedBy,
                 UserRating = x.UserRating                
            };
        }

        private void ValidateRequest(CarIdRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (request.CarId == 0)
            {
                throw new SystemBaseException("CarId is not valid.", SystemErrorCode.ValidationError);
            }
        }

        private void ValidateRequest(CarDetailedRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (string.IsNullOrWhiteSpace(request.Localization))
            {
                throw new SystemBaseException("Localization is not valid.", SystemErrorCode.ValidationError);
            }

            if (request.Mileage < 0)
            {
                throw new SystemBaseException("Mileage is not valid.", SystemErrorCode.ValidationError);
            }

            if (request.PricePerHour < 0.0)
            {
                throw new SystemBaseException("PricePerHour is not valid.", SystemErrorCode.ValidationError);
            }

            if (request.CarModelId == 0)
            {
                throw new SystemBaseException("CarModelId is not valid.", SystemErrorCode.ValidationError);
            }

        }
    }
}
