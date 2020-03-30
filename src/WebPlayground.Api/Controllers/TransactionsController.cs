using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebPlayground.Business.Interfaces;
using WebPlayground.Responses;
using WebPlayground.Shared.Requests;

namespace WebPlayground.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    [Produces("application/json")]
    public class TransactionsController : ControllerBase
    {
        private readonly IOperationsService _operationsService;

        public TransactionsController(IOperationsService service)
        {
            this._operationsService = service;
        }

        [HttpPost]
        [Route("withdraw")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> WithdrawAsync([FromBody]WithdrawRequest request)
        {
            var id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value);

            var response = await this._operationsService.Withdraw(id, request);

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            if (response.Code == ResponseCode.Error)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("transfer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> TransferAsync([FromBody]TransferRequest request)
        {
            var id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value);

            var response = await this._operationsService.Transfer(id, request);

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            if (response.Code == ResponseCode.Error)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("payment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PaymentAsync([FromBody]PaymentRequest request)
        {
            var id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value);

            var response = await this._operationsService.PayBill(id, request);

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            if (response.Code == ResponseCode.Error)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("deposit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DepositAsync([FromBody]DepositRequest request)
        {
            var id = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value);

            var response = await this._operationsService.Deposit(id, request);

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            if (response.Code == ResponseCode.Error)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
