using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Playground.Web.Responses
{
    public enum ResponseCode : byte
    {
        Undefined,

        Success,

        /// <summary>
        /// Erros de negócio (BusinessException, por exemplo)
        /// </summary>
        Error,

        /// <summary>
        /// Exceptions não tratadas (Exception, por exemplo)
        /// </summary>
        Fatal,
    }
}