using System.Threading.Tasks;

namespace MrCoto.Ca.Application.Common.Repositories
{
    public interface IUow
    {
        public Task<int> SaveChanges();
    }
}