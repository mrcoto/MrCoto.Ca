using System.Threading.Tasks;
using MrCoto.Ca.Application.Common.ClientDetection;
using MrCoto.Ca.Application.Common.ClientDetection.Data;
using MrCoto.Ca.Infrastructure.Common.ClientDetection.LocationData;

namespace MrCoto.Ca.AppCli.Common.ClientDetection
{
    public class ConsoleClientInfoDetection : IClientInfoDetection
    {
        public Task<string> Ip()
        {
            return Task.FromResult("0.0.0.0");
        }

        public Task<Location> Location(string ip)
        {
            var location = new Location()
            {
                CountryName = "Consola",
                RegionName = "Consola",
                CityName = "Consola",
                Latitude = 0,
                Longitude = 0
            };
            return Task.FromResult(location);
        }

        public Task<DeviceInfo> DeviceInfo()
        {
            var deviceInfo = new DeviceInfo()
            {
                Device = "Consola",
                DeviceFamily = "Consola",
                DeviceType = "Consola",
                UserAgent = "Consola",
                Browser = "Consola",
                BrowserFamily = "Consola",
            };
            return Task.FromResult(deviceInfo);
        }
    }
}