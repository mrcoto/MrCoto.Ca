using MrCoto.Ca.Domain.Common.Exceptions;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Exceptions
{
    public class InvalidAccountException : BusinessException
    {
        public const string Code = "US:INVALID_ACCOUNT";

        public InvalidAccountException(string message) : base(Code, message)
        {
            
        }

        public InvalidAccountException() : base(Code, "Credenciales Inválidas")
        {
            
        }
        
    }
}