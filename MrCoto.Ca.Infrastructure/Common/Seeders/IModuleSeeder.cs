using System.Threading.Tasks;

namespace MrCoto.Ca.Infrastructure.Common.Seeders
{
    public interface IModuleSeeder
    {
        public Task Run();
    }
}