using System;

namespace MrCoto.Ca.Domain.Common.Exceptions
{
    public class BusinessException : Exception
    {
        public string ExceptionCode { get; set; }

        public BusinessException(string code, string message) : base(message)
        {
            ExceptionCode = code;
        }

        public BusinessException(string message) : base(message)
        {
            
        }
    }
}