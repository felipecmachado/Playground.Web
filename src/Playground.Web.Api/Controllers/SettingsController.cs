using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Playground.Web.Business.Services;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Playground.Web.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/settings")]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService service)
        {
            this._settingsService = service;
        }

        [HttpGet]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSettings(string key = "")
            => Ok(await this._settingsService.GetSettings(key));
        
    }
}
