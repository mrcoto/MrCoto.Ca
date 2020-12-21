using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.Login;
using MrCoto.Ca.WebApi.Common.Controllers;

namespace MrCoto.Ca.WebApi.Modules.GeneralModule.Users
{
    [Route("api/login")]
    public class LoginUserController : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> Handle(LoginUserCommand request)
        {
            var response = await Mediator.Send(request);
            return Ok(new {
                AccessToken = response.AccessToken,
                RefreshToken = response.RefreshToken,
                Message = "Acceso realizado con éxito"
            });
        }
    }
}