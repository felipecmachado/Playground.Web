using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Playground.Web.Business.Interfaces;
using Playground.Web.Shared.Requests;
using System;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Playground.Web.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/accounts")]
    [Produces("application/json")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountManagementService _accountManagementService;
        private readonly ICheckingAccountService _checkingAccountService;

        public AccountsController(IAccountManagementService accountManagementService, ICheckingAccountService checkingAccountService)
        {
            this._accountManagementService = accountManagementService;
            this._checkingAccountService = checkingAccountService;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Create(AccountCreationRequest request)
        {
            //if (product.Description.Contains("XYZ Widget"))
            //{
            //    return BadRequest();
            //}

            //await _repository.AddProductAsync(product);

            //return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);

            return Ok();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update(int checkingAccountId)
        {
            return Ok();
        }

        [HttpGet, Route("{id}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(int id)
        {
            var userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value);
            
            return Ok(await this._accountManagementService.GetCheckingAccount(id, userId));
        }

        [HttpGet, Route("{id}/statement")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStatement(int id)
        {
            var userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value);

            return Ok(await this._checkingAccountService.GetTransactions(userId, id, 30));
        }

        [HttpGet, Route("{id}/recent")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRecentTransactions(int id, int days = 7)
        {
            var userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value);

            return Ok(await this._checkingAccountService.GetTransactions(userId, id, days));
        }
    }
}
