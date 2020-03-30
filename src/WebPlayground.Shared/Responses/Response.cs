using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebPlayground.Responses
{
    [DataContract, Serializable]
    public class Response
    {
        public static readonly Response Success = new Response();

        public Response()
        {
            this.ResponseStatus = new ResponseStatus();
        }

        [DataMember]
        public ResponseStatus ResponseStatus { get; set; }

        [DataMember]
        public ResponseCode Code { get; set; }

        public bool HasErrors()
        {
            switch (this.Code)
            {
                case ResponseCode.Success:
                    return false;

                case ResponseCode.Error:
                case ResponseCode.Fatal:
                    return true;

                case ResponseCode.Undefined:
                default:
                    var status = this.ResponseStatus;
                    return !string.IsNullOrEmpty(status.ErrorCode) ||
                           !string.IsNullOrEmpty(status.Message) ||
                           (status.Errors != null && status.Errors.Any());
            }
        }
    }
}