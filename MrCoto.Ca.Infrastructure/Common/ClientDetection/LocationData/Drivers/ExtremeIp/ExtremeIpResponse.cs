using System.Text.Json.Serialization;

namespace MrCoto.Ca.Infrastructure.Common.ClientDetection.LocationData.Drivers.ExtremeIp
{
    public class ExtremeIpResponse
    {
        [JsonPropertyName("businessName")]   
        public string BusinessName { get; set; }
        [JsonPropertyName("businessWebsite")]   
        public string BusinessWebsite { get; set; }
        [JsonPropertyName("city")]   
        public string City { get; set; }
        [JsonPropertyName("continent")]   
        public string Continent { get; set; }
        [JsonPropertyName("country")]   
        public string Country { get; set; }
        [JsonPropertyName("countryCode")]   
        public string CountryCode { get; set; }
        [JsonPropertyName("ipName")]   
        public string IpName { get; set; }
        [JsonPropertyName("ipType")]   
        public string IpType { get; set; }
        [JsonPropertyName("isp")]   
        public string Isp { get; set; }
        [JsonPropertyName("lat")]   
        public string Lat { get; set; }
        [JsonPropertyName("lon")]   
        public string Lon { get; set; }
        [JsonPropertyName("org")]   
        public string Org { get; set; }
        [JsonPropertyName("query")]   
        public string Query { get; set; }
        [JsonPropertyName("region")]   
        public string Region { get; set; }
        [JsonPropertyName("status")]   
        public string Status { get; set; }
    }
}