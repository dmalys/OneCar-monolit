using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneCarProject.BusinessLayer.Login.Interfaces;
using OneCarProject.BusinessLayer.Login.Models;
using Swashbuckle.Swagger.Annotations;

namespace OneCarProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IAccountHandler _accountHandler;

        public LoginController(ILogger<LoginController> logger,
             IAccountHandler AccountHandler)
        {
            _logger = logger;
            _accountHandler = AccountHandler;
        }

        [HttpPost]
        [Route("GetAccounts")]
        [SwaggerOperation(OperationId = "GetAccounts")]
        [ProducesResponseType(typeof(GetAccountsResponse), StatusCodes.Status200OK)]
        public async Task<GetAccountsResponse> GetAccounts()
        {
            return await _accountHandler.GetAccounts();
        }

        [HttpPost]
        [Route("GetDetails")]
        [SwaggerOperation(OperationId = "GetDetails")]
        [ProducesResponseType(typeof(GetAccountResponse), StatusCodes.Status200OK)]
        public async Task<GetAccountResponse> GetDetails([FromBody] GetAccountRequest request)
        {
            return await _accountHandler.GetAccount(request);
        }

        [HttpPost]
        [Route("UpdateAccount")]
        [SwaggerOperation(OperationId = "UpdateAccount")]
        public async Task UpdateAccount([FromBody]UpdateAccountRequest request)
        {
            await _accountHandler.UpdateAccount(request);
        }

        [HttpPost]
        [Route("AddAccount")]
        [SwaggerOperation(OperationId = "AddAccount")]
        public async Task AddCar([FromBody]AddAccountRequest request)
        {
            await _accountHandler.AddAccount(request);
        }

        [HttpPost]
        [Route("DeleteAccount")]
        [SwaggerOperation(OperationId = "DeleteAccount")]
        public async Task DeleteAccount([FromBody]DeleteAccountRequest request)
        {
            await _accountHandler.DeleteAccount(request);
        }
    }
}
