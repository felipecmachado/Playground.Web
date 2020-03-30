using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Playground.Web.Responses
{
    [DataContract]
    public class ResponseError
    {
        [DataMember]
        public string ErrorCode { get; set; }

        [DataMember]
        public string FieldName { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}