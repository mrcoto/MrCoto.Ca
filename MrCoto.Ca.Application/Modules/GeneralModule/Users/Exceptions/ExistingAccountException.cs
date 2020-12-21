using MrCoto.Ca.Domain.Common.Exceptions;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Exceptions
{
    public class ExistingAccountException : BusinessException
    {
        public const string Code = "US:EXISTING_ACCOUNT";

        public ExistingAccountException(string message) : base(Code, message)
        {
            
        }

        public ExistingAccountException() : base(Code, "Esta cuenta ya existe")
        {
            
        }
        
    }
}