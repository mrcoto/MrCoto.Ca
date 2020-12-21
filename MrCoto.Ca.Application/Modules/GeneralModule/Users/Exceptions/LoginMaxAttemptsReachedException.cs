using MrCoto.Ca.Domain.Common.Exceptions;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Exceptions
{
    public class LoginMaxAttemptsReachedException : BusinessException
    {
        public const string Code = "US:LOGIN_MAX_ATTEMPTS_REACHED";

        public LoginMaxAttemptsReachedException(string message) : base(Code, message)
        {
            
        }

        public LoginMaxAttemptsReachedException() : base(Code, "Se ha superado la cantidad de intentos de inicio de sesión")
        {
            
        }
        
    }
}