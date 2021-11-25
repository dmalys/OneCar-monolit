﻿using OneCarProject.BusinessLayer.Car.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneCarProject.BusinessLayer.Car.Interfaces
{
    public interface ICarHandler
    {
        Task<GetCarResponse> GetCar(GetCarRequest request);

        Task UpdateCar(UpdateCarRequest request);

        Task DeleteCar(DeleteCarRequest request);

        Task AddCar(AddCarRequest request);

        Task<GetCarsResponse> GetCars();
    }
}
