using System.Threading.Tasks;

namespace MrCoto.Ca.Infrastructure.Common.Seeders
{
    public interface ITableSeeder
    {
        public Task Seed();
    }
}