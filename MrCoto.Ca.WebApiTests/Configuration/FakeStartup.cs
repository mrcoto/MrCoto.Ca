using System.IO;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MrCoto.Ca.Application;
using MrCoto.Ca.WebApi.Common.Exceptions;

namespace MrCoto.Ca.WebApiTests.Configuration
{
    public class FakeStartup
    {
        public FakeStartup()
        {
            Configuration = Configuration = new ConfigurationBuilder()  
                .SetBasePath(Directory.GetCurrentDirectory())  
                .AddJsonFile("appsettings.Test.json")  
                .Build();  ;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication()
                .AddFakeInfrastructure(Configuration);
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        
            app.UseHttpsRedirection();
        
            app.UseDetection();
            
            app.UseRouting();
        
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            
            app.UseCors(x => x
                .SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        
            app.UseMiddleware<ExceptionHandler>();
            
            app.UseAuthentication();
            app.UseAuthorization();
        
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
