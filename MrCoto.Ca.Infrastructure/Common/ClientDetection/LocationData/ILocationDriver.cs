using System.Threading.Tasks;
using MrCoto.Ca.Application.Common.ClientDetection.Data;

namespace MrCoto.Ca.Infrastructure.Common.ClientDetection.LocationData
{
    public interface ILocationDriver
    {
        public Task<Location> GetLocation(string ip);
    }
}