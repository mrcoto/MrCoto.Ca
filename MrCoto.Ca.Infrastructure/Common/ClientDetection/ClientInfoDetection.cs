using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MrCoto.Ca.Application.Common.ClientDetection;
using MrCoto.Ca.Application.Common.ClientDetection.Data;
using MrCoto.Ca.Infrastructure.Common.ClientDetection.LocationData;
using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;

namespace MrCoto.Ca.Infrastructure.Common.ClientDetection
{
    public class ClientInfoDetection : IClientInfoDetection
    {
        public string DefaultIp = "0.0.0.0";
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDetectionService _detectionService;

        public ClientInfoDetection(IHttpContextAccessor httpContextAccessor, IDetectionService detectionService)
        {
            _httpContextAccessor = httpContextAccessor;
            _detectionService = detectionService;
        }

        public Task<string> Ip()
        {
            var ip = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            ip = string.IsNullOrEmpty(ip) ? DefaultIp : ip;
            return Task.FromResult(ip);
        }

        public async Task<Location> Location(string ip) => await LocationFactory.Location(ip);

        public Task<DeviceInfo> DeviceInfo()
        {
            var deviceInfo = new DeviceInfo()
            {
                Device = _detectionService.Platform.Name.ToString(),
                DeviceFamily = _detectionService.Platform.Name.ToString(),
                DeviceType = GetDeviceType(_detectionService.Device.Type),
                UserAgent = _detectionService.UserAgent.ToString(),
                Browser = _detectionService.Browser.Name.ToString(),
                BrowserFamily = _detectionService.Browser.Name.ToString()
            };
            return Task.FromResult(deviceInfo);
        }

        private string GetDeviceType(Device device)
        {
            if (device == Device.Car) return "Automóvil";
            if (device == Device.Console) return "Consola";
            if (device == Device.Desktop) return "Escritorio";
            if (device == Device.Mobile) return "Móvil";
            if (device == Device.Tablet) return "Tablet";
            if (device == Device.Tv) return "Tv";
            if (device == Device.Unknown) return "Desconocido";
            if (device == Device.Watch) return "Watch";
            if (device == Device.IoT) return "IoT";
            return "Desconocido";
        }
    }
}