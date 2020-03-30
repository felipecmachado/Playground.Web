using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebPlayground.Data;
using WebPlayground.Domain.Management;
using WebPlayground.Infrastructure;
using WebPlayground.Responses;
using WebPlayground.Shared.Common;
using WebPlayground.Shared.Responses;

namespace WebPlayground.Business.Interfaces
{
    public class UserService : BaseService, IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, BankContext context) : base(context)
        {
            _appSettings = appSettings.Value;
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

        public Task<Response<User>> GetById()
        {
            throw new NotImplementedException();
        }

        private async Task UpdateLastAccess(User user)
        {
            user.LastAccessAt = DateTime.UtcNow;

            await this.Context.SaveChangesAsync();
        }
    }
}
