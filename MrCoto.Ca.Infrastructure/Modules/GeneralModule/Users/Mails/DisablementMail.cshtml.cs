using MrCoto.Ca.Application.Modules.GeneralModule.Users.Mails.Disablement;
using MrCoto.Ca.Infrastructure.Common.Pages.Shared;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Mails
{
    public class DisablementMailModel : _LayoutModel, IDisablementMail
    {
        public void OnGet()
        {
        }

        public DisablementMailData Data { get; set; }
    }
}
