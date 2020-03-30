using System.Collections.Generic;
using System.Threading.Tasks;
using WebPlayground.Domain.Management;
using WebPlayground.Responses;
using WebPlayground.Shared.Responses;

namespace WebPlayground.Business.Interfaces
{
    public interface IUserService
    {
        Task<AuthResponse> Authenticate(string login, string password);

        Task<Response<User>> GetById();

        Task<Response<IList<User>>> GetAll();
    }
}
