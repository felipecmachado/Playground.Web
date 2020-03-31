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
    [Route("api/management")]
    [Produces("application/json")]
    public class ManagementController : ControllerBase
    {
        private readonly IAccountManagementService _accountManagementService;

        public ManagementController(IAccountManagementService accountManagementService)
        {
            this._accountManagementService = accountManagementService;
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("accounts")]
        public async Task<IActionResult> GetAllCheckingAccounts()
            => Ok(await this._accountManagementService.GetAllCheckingAccounts());
    }
}
