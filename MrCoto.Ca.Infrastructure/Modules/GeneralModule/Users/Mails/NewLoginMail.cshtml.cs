using MrCoto.Ca.Application.Modules.GeneralModule.Users.Mails.NewLogin;
using MrCoto.Ca.Infrastructure.Common.Pages.Shared;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Users.Mails
{
    public class NewLoginMailModel : _LayoutModel, INewLoginMail
    {
        public void OnGet()
        {
        }

        public NewLoginMailData Data { get; set; }
    }
}
