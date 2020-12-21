using MrCoto.Ca.Application.Modules.GeneralModule.Users.Mails.Register;
using MrCoto.Ca.Infrastructure.Common.Pages.Shared;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Mails
{
    public class RegisterMailModel : _LayoutModel, IRegisterMail
    {
        public void OnGet()
        {
        }

        public RegisterMailData Data { get; set; }
    }
}
