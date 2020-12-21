using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrCoto.Ca.Application.Common.ClientDetection;
using MrCoto.Ca.WebApi.Common.Controllers;

namespace MrCoto.Ca.WebApi.Modules.GeneralModule.Users
{
    [Route("api/device_info")]
    public class ClientInfoController : ApiController
    {
        private readonly IClientInfoDetection _clientInfoDetection;

        public ClientInfoController(IClientInfoDetection clientInfoDetection)
        {
            _clientInfoDetection = clientInfoDetection;
        }

        [HttpPost]
        public async Task<IActionResult> Handle()
        {
            var ip = await _clientInfoDetection.Ip();
            return Ok(new {
                Ip = ip,
                Location = await _clientInfoDetection.Location(ip),
                DeviceInfo = await _clientInfoDetection.DeviceInfo()
            });
        }
    }
}