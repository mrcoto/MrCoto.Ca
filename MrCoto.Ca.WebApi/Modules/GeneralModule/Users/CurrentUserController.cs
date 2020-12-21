using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrCoto.Ca.Application.Common.CurrentUsers;
using MrCoto.Ca.WebApi.Common.Controllers;

namespace MrCoto.Ca.WebApi.Modules.GeneralModule.Users
{
    [Authorize]
    [Route("api/user/me")]
    public class CurrentUserController : ApiController
    {
        private readonly ICurrentUserService _currentUserService;

        public CurrentUserController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Handle()
        {
            var currentUser = await _currentUserService.CurrentUser();
            return Ok(currentUser);
        }
    }
}