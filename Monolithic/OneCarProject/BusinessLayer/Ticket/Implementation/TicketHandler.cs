using OneCarProject.BusinessLayer.ErrorHandling;
using OneCarProject.BusinessLayer.Ticket.Interfaces;
using OneCarProject.BusinessLayer.Ticket.Models;
using OneCarProject.DataAccessLayer.Entities;
using OneCarProject.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneCarProject.BusinessLayer.Ticket.Implementation
{
    public class TicketHandler : ITicketHandler
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ICarRepository _carRepository;

        public TicketHandler(ITicketRepository ticketRepository, ICarRepository carRepository)
        {
            _ticketRepository = ticketRepository;
            _carRepository = carRepository;
        }

        public async Task AddTicket(AddTicketRequest request)
        {
            ValidateRequest(request);

            var ticketEntity = new TicketEntity();
            ticketEntity.ExpirationDate = request.ExpirationDate;
            ticketEntity.CreateDate = DateTime.UtcNow;
            ticketEntity.CreatedBy = request.CreatedBy;
            ticketEntity.CarId = request.CarId;

            try
            {
                if(await _carRepository.CheckCarExists(request.CarId))
                {
                    await _ticketRepository.Insert(ticketEntity);

                }
                else
                {
                    throw new SystemBaseException("Car entity is not existing.", SystemErrorCode.EntityNotFound);
                }
            }
            catch (SystemBaseException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new SystemBaseException("AddTicket failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task DeleteTicket(DeleteTicketRequest request)
        {
            ValidateRequest(request);

            try
            {
                if (await _ticketRepository.CheckTicketExists(request.TicketId))
                {
                    await _ticketRepository.DeleteAsync(request.TicketId);
                }
            }
            catch (Exception)
            {
                throw new SystemBaseException("DeleteTicket failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task<GetTicketResponse> GetTicket(GetTicketRequest request)
        {
            ValidateRequest(request);

            try
            {
                var ticketEntity = await _ticketRepository.GetAsync(request.TicketId);

                if (ticketEntity == null)
                {
                    return new GetTicketResponse();
                }

                return new GetTicketResponse
                {
                    Ticket = ConvertFromEntity(ticketEntity)
                };
            }
            catch (Exception)
            {
                throw new SystemBaseException("GetTicket failed.", SystemErrorCode.SystemError);
            }            
        }

        public async Task<GetTicketsResponse> GetTickets()
        {
            try
            {
                var allEntities = await _ticketRepository.GetAll();

                if(allEntities.Count == 0)
                {
                    return new GetTicketsResponse { TicketList = new List<TicketDTO>() };
                }

                return ConvertEntitiesToResponse(allEntities);
            }
            catch (Exception)
            {
                throw new SystemBaseException("GetTickets failed.", SystemErrorCode.SystemError);
            }
        }

        private GetTicketsResponse ConvertEntitiesToResponse(List<TicketEntity> allEntities)
        {
            var entities = allEntities.Select(x => ConvertFromEntity(x)).ToList();
            return new GetTicketsResponse
            {
                TicketList = entities
            };
        }

        private TicketDTO ConvertFromEntity(TicketEntity x)
        {
            return new TicketDTO
            {
                TicketId = x.TicketId,
                CarId = x.CarId,
                CreateDate = x.CreateDate,
                CreatedBy = x.CreatedBy,
                UpdateDate = x.UpdateDate,
                UpdatedBy = x.UpdatedBy,
                ExpirationDate = x.ExpirationDate
            };
        }

        private void ValidateRequest(TicketIdRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (request.TicketId == 0)
            {
                throw new SystemBaseException("TicketId is not valid.", SystemErrorCode.ValidationError);
            }
        }

        private void ValidateRequest(AddTicketRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (request.CarId == 0)
            {
                throw new SystemBaseException("CarId is not valid.", SystemErrorCode.ValidationError);
            }

            if (string.IsNullOrWhiteSpace(request.CreatedBy))
            {
                throw new SystemBaseException("CreatedBy is not valid.", SystemErrorCode.ValidationError);
            }
        }
    }
}
