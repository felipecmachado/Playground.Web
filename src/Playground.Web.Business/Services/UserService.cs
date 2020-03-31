using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Playground.Web.Data;
using Playground.Web.Domain.Management;
using Playground.Web.Infrastructure;
using Playground.Web.Responses;
using Playground.Web.Shared.Common;
using Playground.Web.Shared.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Playground.Web.Business.Interfaces
{
    public class UserService : BaseService, IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(AppSettings appSettings, BankContext context) : base(context)
        {
            _appSettings = appSettings;
        }

        public async Task<AuthResponse> Authenticate(string login, string password)
        {
            var response = new AuthResponse();

            var user = await this.Context.Users.SingleOrDefaultAsync(x => x.Login == login && x.Password == password);

            if (user == null)
                return null;

            response.UserId = user.UserId;
            response.Login = user.Login;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Email, user.EmailAddress)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            response.Token = tokenHandler.WriteToken(token);

            await this.UpdateLastAccess(user);

            return response;
        }

        public async Task<Response<IList<User>>> GetAll()
            => new Response<IList<User>>() { Code = ResponseCode.Success, Item = await this.Context.Users.ToListAsync() };

        public async Task<Response<User>> GetById(int id)
            => new Response<User>() { Code = ResponseCode.Success, Item = await this.Context.Users.Include(x => x.CheckingAccount).FirstOrDefaultAsync(x => x.UserId == id) };

        private async Task UpdateLastAccess(User user)
        {
            user.LastAccessAt = DateTime.UtcNow;

            await this.Context.SaveChangesAsync();
        }
    }
}
