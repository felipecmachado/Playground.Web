using System;

namespace WebPlayground.Shared.Exceptions
{
    public class BusinessException : ApplicationException
    {
        public BusinessException(string field, string message) : base(message) { this.FieldName = field; }
        public BusinessException(string message, Exception innerException) : base(message, innerException) { }
        public string FieldName { get; set; }
    }
}
