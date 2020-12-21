using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using MrCoto.Ca.Application.Common.ClientDetection.Data;

namespace MrCoto.Ca.Infrastructure.Common.ClientDetection.LocationData.Drivers.ExtremeIp
{
    public class ExtremeIpDriver : ILocationDriver
    {
        public const string Url = "https://extreme-ip-lookup.com/json/";

        public async Task<Location> GetLocation(string ip)
        {
            var http = new HttpClient();
            var httpResponse = await http.GetAsync(Url + ip);
            var content = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<ExtremeIpResponse>(content);
            return new Location()
            {
                CountryName = response.Country,
                RegionName = response.Region,
                CityName = response.City,
                Latitude = string.IsNullOrWhiteSpace(response.Lat) ? 0 : double.Parse(response.Lat),
                Longitude = string.IsNullOrWhiteSpace(response.Lon) ? 0 : double.Parse(response.Lon),
            };
        }
    }
}