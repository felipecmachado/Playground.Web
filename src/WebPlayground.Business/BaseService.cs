using System;
using System.Collections.Generic;
using System.Text;
using WebPlayground.Infrastructure;

namespace WebPlayground.Data
{
    public abstract class BaseService
    {
        public BankContext Context { get; set; }

        public BaseService(BankContext context)
        {
            this.Context = context;
        }
    }
}

