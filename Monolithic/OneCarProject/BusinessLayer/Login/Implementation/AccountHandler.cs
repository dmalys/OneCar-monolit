using OneCarProject.BusinessLayer.ErrorHandling;
using OneCarProject.BusinessLayer.Login.Interfaces;
using OneCarProject.BusinessLayer.Login.Models;
using OneCarProject.DataAccessLayer.Entities;
using OneCarProject.DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneCarProject.BusinessLayer.Login.Implementation
{
    public class AccountHandler : IAccountHandler
    {
        private readonly IAccountRepository _accountRepository;

        public AccountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task AddAccount(AddAccountRequest request)
        {
            ValidateRequest(request);

            var accountEntity = new AccountEntity();
            accountEntity.PricePerMonth = request.PricePerMonth;
            accountEntity.Discount = request.Discount;
            accountEntity.AccountType = request.AccountType;
            accountEntity.CreateDate = DateTime.UtcNow;

            try
            {
                await _accountRepository.Insert(accountEntity);
            }
            catch (Exception)
            {
                throw new SystemBaseException("AddAccount failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task UpdateAccount(UpdateAccountRequest request)
        {
            ValidateRequest(request);
            if (request.AccountId == 0)
            {
                throw new SystemBaseException("AccountId is not valid.", SystemErrorCode.ValidationError);
            }

            var accountEntity = new AccountEntity();
            accountEntity.PricePerMonth = request.PricePerMonth;
            accountEntity.Discount = request.Discount;
            accountEntity.AccountType = request.AccountType;
            accountEntity.UpdateDate = DateTime.UtcNow;

            try
            {
                if (await _accountRepository.CheckAccountExists(request.AccountId))
                {
                    await _accountRepository.Update(accountEntity);
                }
                else
                {
                    throw new SystemBaseException("Entity not found.", SystemErrorCode.EntityNotFound);
                }
            }
            catch (Exception)
            {
                throw new SystemBaseException("UpdateAccount failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task DeleteAccount(DeleteAccountRequest request)
        {
            ValidateRequest(request);

            try
            {
                if (await _accountRepository.CheckAccountExists(request.AccountId))
                {
                    await _accountRepository.DeleteAsync(request.AccountId);
                }
            }
            catch (Exception)
            {
                throw new SystemBaseException("DeleteAccount failed.", SystemErrorCode.SystemError);
            }
        }

        public async Task<GetAccountResponse> GetAccount(GetAccountRequest request)
        {
            ValidateRequest(request);

            try
            {
                var accountEntity = await _accountRepository.GetAsync(request.AccountId);

                if(accountEntity == null)
                {
                    return new GetAccountResponse();
                }

                return new GetAccountResponse
                {
                    Account = ConvertFromEntity(accountEntity)
                };
            }
            catch (Exception)
            {
                throw new SystemBaseException("GetAccount failed.", SystemErrorCode.SystemError);
            }           
        }

        public async Task<GetAccountsResponse> GetAccounts()
        {
            try
            {
                var allEntities = await _accountRepository.GetAll();

                if(allEntities.Count == 0)
                {
                    return new GetAccountsResponse { AccountList = new List<AccountDTO>() };
                }

                return ConvertEntitiesToResponse(allEntities);
            }
            catch (Exception)
            {
                throw new SystemBaseException("GetAccounts failed.", SystemErrorCode.SystemError);
            }
        }

        private GetAccountsResponse ConvertEntitiesToResponse(List<AccountEntity> allEntities)
        {
            var entities = allEntities.Select(x => ConvertFromEntity(x)).ToList();
            return new GetAccountsResponse
            {
                AccountList = entities
            };
        }

        private AccountDTO ConvertFromEntity(AccountEntity x)
        {
            return new AccountDTO
            {
                AccountId = x.AccountId,
                AccountType = x.AccountType,
                Discount = x.Discount,
                PricePerMonth = x.PricePerMonth,
                CreateDate = x.CreateDate,
                UpdateDate = x.UpdateDate
            };
        }

        private void ValidateRequest(AccountIdRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (request.AccountId == 0)
            {
                throw new SystemBaseException("AccountId is not valid.", SystemErrorCode.ValidationError);
            }
        }

        private void ValidateRequest(AccountDetailedRequest request)
        {
            if (request == null)
            {
                throw new SystemBaseException("Request can not be null.", SystemErrorCode.ValidationError);
            }

            if (request.Discount <= 0)
            {
                throw new SystemBaseException("Discount is not valid.", SystemErrorCode.ValidationError);
            }

            if (string.IsNullOrWhiteSpace(request.AccountType))
            {
                throw new SystemBaseException("AccountType is not valid.", SystemErrorCode.ValidationError);
            }
        }
    }
}
