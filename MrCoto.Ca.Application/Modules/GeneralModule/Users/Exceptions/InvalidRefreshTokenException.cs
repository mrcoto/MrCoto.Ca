using MrCoto.Ca.Domain.Common.Exceptions;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Exceptions
{
    public class InvalidRefreshTokenException : BusinessException
    {
        public const string Code = "US:INVALID_REFRESH_TOKEN";

        public InvalidRefreshTokenException(string message) : base(Code, message)
        {
            
        }

        public InvalidRefreshTokenException() : base(Code, "Token de refresco inválido")
        {
            
        }
        
    }
}