using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Playground.Web.Business.Interfaces;
using Playground.Web.Shared.Requests;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Playground.Web.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    [Produces("application/json")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountManagementService _accountManagementService;

        public AccountsController(IAccountManagementService accountManagementService)
        {
            this._accountManagementService = accountManagementService;
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
            => Ok(await this._accountManagementService.GetCheckingAccount(id));

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
            => Ok(await this._accountManagementService.GetAllCheckingAccounts());

        [HttpGet, Route("statement")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetStatement(int checkingAccountId)
        {
            return Ok();
        }
    }
}
