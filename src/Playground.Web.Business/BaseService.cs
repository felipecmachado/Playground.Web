using System;
using System.Collections.Generic;
using System.Text;
using Playground.Web.Domain.CheckingAccount;
using Playground.Web.Infrastructure;
using Playground.Web.Responses;

namespace Playground.Web.Data
{
    public abstract class BaseService
    {
        public BankContext Context { get; set; }

        public BaseService(BankContext context)
        {
            this.Context = context;
        }

        public ResponseStatus ValidateAccount(CheckingAccount account, string token)
        {
            var response = new ResponseStatus();

            if (account == null)
            {
                response.AddError("AccountNumber", "The account number is invalid.");
                return response;
            }

            if (!account.IsTokenValid(token))
            {
                // TODO: After three invalid tokens it should block the account for 15 minutes
                response.AddError("Token", "Token is invalid.");
                return response;
            }

            return response;
        }
    }
}

