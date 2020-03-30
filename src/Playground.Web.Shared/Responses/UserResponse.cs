using System;

namespace Playground.Web.Shared.Responses
{
    public class AuthResponse
    { 
        public int UserId { get; set; }

        public string Login { get; set; }

        public string Token { get; set; }
    }
}