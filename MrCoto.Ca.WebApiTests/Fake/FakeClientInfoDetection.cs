using System.Threading.Tasks;
using MrCoto.Ca.Application.Common.ClientDetection;
using MrCoto.Ca.Application.Common.ClientDetection.Data;

namespace MrCoto.Ca.WebApiTests.Fake
{
    public class FakeClientInfoDetection : IClientInfoDetection
    {
        private const string DefaultValue = "Testing";
        
        public Task<string> Ip()
        {
            return Task.FromResult("0.0.0.1");
        }

        public Task<Location> Location(string ip)
        {
            var location = new Location()
            {
                CountryName = DefaultValue,
                RegionName = DefaultValue,
                CityName = DefaultValue,
                Latitude = 0,
                Longitude = 0
            };
            return Task.FromResult(location);
        }

        public Task<DeviceInfo> DeviceInfo()
        {
            var deviceInfo = new DeviceInfo()
            {
                Browser = DefaultValue,
                BrowserFamily = DefaultValue,
                Device = DefaultValue,
                DeviceFamily = DefaultValue,
                DeviceType = DefaultValue,
                UserAgent = DefaultValue
            };
            return Task.FromResult(deviceInfo);
        }
    }
}