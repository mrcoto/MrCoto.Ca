using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrCoto.Ca.Application.Modules.GeneralModule.Users.Commands.RefreshToken;
using MrCoto.Ca.WebApi.Common.Controllers;

namespace MrCoto.Ca.WebApi.Modules.GeneralModule.Users
{
    [Route("api/refresh_token")]
    public class RefreshTokenController : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> Handle(RefreshTokenCommand request)
        {
            var response = await Mediator.Send(request);
            return Ok(new {
                AccessToken = response.AccessToken,
                RefreshToken = response.RefreshToken,
                Message = "Token refrescado con éxito"
            });
        }
    }
}