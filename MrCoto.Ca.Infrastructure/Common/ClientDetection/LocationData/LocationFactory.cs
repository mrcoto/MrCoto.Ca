using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MrCoto.Ca.Application.Common.ClientDetection.Data;
using MrCoto.Ca.Infrastructure.Common.ClientDetection.LocationData.Drivers.ExtremeIp;

namespace MrCoto.Ca.Infrastructure.Common.ClientDetection.LocationData
{
    public class LocationFactory
    {
        public static async Task<Location> Location(string ip) => await (new LocationFactory()).GetLocation(ip);

        public async Task<Location> GetLocation(string ip)
        {
            foreach (var driver in _drivers)
            {
                try
                {
                    return await driver.GetLocation(ip);
                }
                catch (Exception)
                {
                    // Fallback. Continue until find some.
                }
            }

            return DefaultLocation();
        }

        private Location DefaultLocation()
        {
            return new Location()
            {
                CountryName = "Desconocido o Local",
                RegionName = "Desconocido o Local",
                CityName = "Desconocido o Local",
                Latitude = 0,
                Longitude = 0
            };
        }
        
        private readonly List<ILocationDriver> _drivers = new List<ILocationDriver>()
        {
            new ExtremeIpDriver(),
        };
    }
}