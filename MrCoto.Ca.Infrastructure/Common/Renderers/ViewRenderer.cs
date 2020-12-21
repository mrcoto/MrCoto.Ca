using System.Threading.Tasks;
using MrCoto.Ca.Application.Common.Renderers;
using RazorLight;

namespace MrCoto.Ca.Infrastructure.Common.Renderers
{
    public class ViewRenderer : IViewRenderer
    {
        private readonly string _rootNamespace;
        private readonly RazorLightEngine _razor;
        
        public ViewRenderer()
        {
            var root = typeof(DependencyInjection);
            _rootNamespace = root.Namespace ?? "";
            _razor = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(root)
                .UseMemoryCachingProvider()
                .Build();
        }
        
        public async Task<string> Render<TData>(string className, TData data)
        {
            var relativeClassName = className;
            if (!string.IsNullOrEmpty(_rootNamespace))
            {
                relativeClassName = className.Replace(_rootNamespace + ".", "");
            }
            return await _razor.CompileRenderAsync(relativeClassName, data);
        }
    }
}