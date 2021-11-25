using OneCarProject.BusinessLayer.CarImage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneCarProject.BusinessLayer.CarImage.Models;
using OneCarProject.DataAccessLayer.Interfaces;
using OneCarProject.DataAccessLayer.Entities;
using System.IO;
using Microsoft.AspNetCore.Http;
using OneCarProject.BusinessLayer.ErrorHandling;

namespace OneCarProject.BusinessLayer.CarImage.Implementation
{
    public class CarImageHandler : ICarImageHandler
    {
        private readonly ICarImageRepository _carImageRepository;
        private List<string> AcceptableExtensions = new List<string> { ".png", ".jpg", ".jpeg" };

        public CarImageHandler(ICarImageRepository carImageRepository)
        {
            _carImageRepository = carImageRepository;
        }

        public async Task AddCarImage(AddCarImageRequest request)
        {
            ValidateRequest(request);
            ValidateFile(request.Content);
            
            try
            {
                using (var ms = new MemoryStream())
                {
                    request.Content.CopyTo(ms);
                    var carImageEntity = new CarImageEntity();
                    carImageEntity.FileName = request.FileName;
                    carImageEntity.CreateDate = DateTime.UtcNow;
                    carImageEntity.CreatedBy = request.CreatedBy;//userName;
                    carImageEntity.Content = ms.ToArray();

                    await _carImageRepository.Insert(carImageEntity);
                }
            }
            catch (Exception)
            {
                throw new SystemBaseException("AddCarImage failed.", SystemErrorCode.SystemError);
            }                 
        }

        public async Task DeleteCarImage(DeleteCarImageRequest request)
        {
            ValidateRequest(request);

            try
            {
                if (await _carImageRepository.CheckCarImageExists(request.CarImageId))
                {
                    await _carImageRepository.DeleteAsync(request.CarImageId);
                }
            }
            catch (Exception)
            {
                throw new SystemBaseException("DeleteCarImage failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task<GetCarImageResponse> GetCarImage(GetCarImageRequest request)
        {
            ValidateRequest(request);

            try
            {
                var carEntity = await _carImageRepository.GetAsync(request.CarImageId);

                if(carEntity == null)
                {
                    return new GetCarImageResponse();
                }

                return new GetCarImageResponse
                {
                    CarImage = ConvertFromEntity(carEntity)
                };
            }
            catch (Exception)
            {
                throw new SystemBaseException("GetCarImage failed.", SystemErrorCode.SystemError);
            }            
        }

        public async Task<GetCarImagesResponse> GetCarImages()
        {
            try
            {
                var allEntities = await _carImageRepository.GetAll();

                if(allEntities.Count == 0)
                {
                    return new GetCarImagesResponse { CarImageList = new List<CarImageDTO>() };
                }

                return ConvertEntitiesToResponse(allEntities);
            }
            catch (Exception)
            {
                throw new SystemBaseException("GetCarImages failed.", SystemErrorCode.SystemError);
            }            
        }

        public async Task UpdateCarImage(UpdateCarImageRequest request)
        {
            ValidateRequest(request);
            ValidateFile(request.Content);
            if (request.CarImageId == 0)
            {
                throw new SystemBaseException("CarImageId is not valid", SystemErrorCode.ValidationError);
            }

            try
            {
                if (await _carImageRepository.CheckCarImageExists(request.CarImageId))
                {
                    using (var ms = new MemoryStream())
                    {
                        request.Content.CopyTo(ms);
                        var carImageEntity = new CarImageEntity();
                        carImageEntity.FileName = request.FileName;
                        carImageEntity.Content = ms.ToArray();
                        carImageEntity.UpdateDate = DateTime.UtcNow;
                        carImageEntity.UpdatedBy = request.UpdatedBy;//userName;


                        await _carImageRepository.Update(carImageEntity);
                    }
                }
            }
            catch (Exception)
            {
                throw new SystemBaseException("UpdateCarImage failed.", SystemErrorCode.SystemError);
            }            
        }

        private void ValidateFile(IFormFile content)
        {
            //validation
            if (content == null || content.Length == 0)
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);

            //Getting FileName
            var fileName = Path.GetFileName(content.FileName);
            //Getting file Extension
            var fileExtension = Path.GetExtension(fileName);

            if (!AcceptableExtensions.Contains(fileExtension))
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
        }

        private GetCarImagesResponse ConvertEntitiesToResponse(List<CarImageEntity> allentities)
        {
            var entities = allentities.Select(x => ConvertFromEntity(x)).ToList();
            return new GetCarImagesResponse
            {
                CarImageList = entities
            };
        }

        private CarImageDTO ConvertFromEntity(CarImageEntity x)
        {
            return new CarImageDTO
            {
                CarImageId = x.CarImageId,
                Content = x.Content,
                CreateDate = x.CreateDate,
                CreatedBy = x.CreatedBy,
                UpdateDate = x.UpdateDate,
                UpdatedBy = x.UpdatedBy,
                FileName = x.FileName
            };
        }

        private void ValidateRequest(CarImageIdRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (request.CarImageId == 0)
            {
                throw new SystemBaseException("CarImageId is not valid.", SystemErrorCode.ValidationError);
            }
        }

        private void ValidateRequest(CarImageDetailedRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }
            
            if (string.IsNullOrWhiteSpace(request.FileName))
            {
                throw new SystemBaseException("FileName is not valid.", SystemErrorCode.ValidationError);
            }
        }
    }
}
