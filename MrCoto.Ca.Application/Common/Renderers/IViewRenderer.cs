using System.Threading.Tasks;

namespace MrCoto.Ca.Application.Common.Renderers
{
    public interface IViewRenderer
    {
        public Task<string> Render<TData>(string className, TData data);
    }
}