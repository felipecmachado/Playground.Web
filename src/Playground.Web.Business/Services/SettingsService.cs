using Microsoft.EntityFrameworkCore;
using Playground.Web.Data;
using Playground.Web.Domain.Management;
using Playground.Web.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playground.Web.Business.Services
{
    public class SettingsService : BaseService, ISettingsService
    {
        public SettingsService(BankContext context) : base(context)  { }

        public async Task<IList<Settings>> GetSettings(string key)
           => await this.Context.Settings.Where(x => string.IsNullOrEmpty(key) || x.Key == key).ToListAsync();
    }
}
