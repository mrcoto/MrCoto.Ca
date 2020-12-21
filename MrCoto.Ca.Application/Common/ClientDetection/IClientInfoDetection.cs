using System.Threading.Tasks;
using MrCoto.Ca.Application.Common.ClientDetection.Data;

namespace MrCoto.Ca.Application.Common.ClientDetection
{
    public interface IClientInfoDetection
    {
        public Task<string> Ip();
        public Task<Location> Location(string ip);
        public Task<DeviceInfo> DeviceInfo();

    }
}