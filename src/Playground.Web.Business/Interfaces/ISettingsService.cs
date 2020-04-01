using Playground.Web.Domain.Management;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Web.Business.Services
{
    public interface ISettingsService
    {
        Task<IList<Settings>> GetSettings(string key);
    }
}
