using MediatR;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.DisableUser
{
    public class DisableUserCommand : IRequest<UserDisablement>
    {
        public long DisablementTypeId { get; set; }
        public string Observation { get; set; }
        
        public long ToDisableId { get; set; }
        
        public long DisabledById { get; set; }
        
    }
}