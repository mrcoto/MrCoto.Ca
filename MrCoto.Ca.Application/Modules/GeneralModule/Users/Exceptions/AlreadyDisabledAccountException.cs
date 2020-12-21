using MrCoto.Ca.Domain.Common.Exceptions;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Exceptions
{
    public class AlreadyDisabledAccountException : BusinessException
    {
        public const string Code = "US:ALREADY_DISABLED";

        public AlreadyDisabledAccountException(string message) : base(Code, message)
        {
            
        }

        public AlreadyDisabledAccountException() : base(Code, "Esta cuenta ya ha sido deshabilitada")
        {
            
        }
        
    }
}