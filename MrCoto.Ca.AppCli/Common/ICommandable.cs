using System.Threading.Tasks;

namespace MrCoto.Ca.AppCli.Common
{
    public interface ICommandable
    {
        public abstract Task OnExecute();
    }
}