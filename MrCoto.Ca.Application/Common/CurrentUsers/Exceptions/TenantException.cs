using MrCoto.Ca.Domain.Common.Exceptions;

namespace MrCoto.Ca.Application.Common.CurrentUsers.Exceptions
{
    public class TenantException : BusinessException
    {
        public const string Code = "SYS:INVALID_TENANT";

        public TenantException() : base(Code, "No perteneces a esta Organización")
        {
            
        }
        
    }
}