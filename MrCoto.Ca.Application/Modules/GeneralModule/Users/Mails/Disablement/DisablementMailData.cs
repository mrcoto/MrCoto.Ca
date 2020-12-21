using MrCoto.Ca.Domain.Modules.GeneralModule.Users;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Mails.Disablement
{
    public class DisablementMailData
    {
        public long DisablementTypeId { get; set; }
        public string DisablementType { get; set; }
        public string Username { get; set; }
        public string Observation { get; set; }
    }
}