using System.Collections.Generic;
using System.Threading.Tasks;
using Playground.Web.Domain.Management;
using Playground.Web.Responses;
using Playground.Web.Shared.Responses;

namespace Playground.Web.Business.Interfaces
{
    public interface IUserService
    {
        Task<AuthResponse> Authenticate(string login, string password);

        Task<Response<User>> GetById(int id);

        Task<Response<IList<User>>> GetAll();
    }
}
