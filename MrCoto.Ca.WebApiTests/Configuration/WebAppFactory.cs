using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MrCoto.Ca.Infrastructure;
using MrCoto.Ca.Infrastructure.Modules.GeneralModule.Configuration;

namespace MrCoto.Ca.WebApiTests.Configuration
{
    public class WebAppFactory : WebApplicationFactory<FakeStartup>
    {
        public readonly string InMemoryDb;
        public TestUtil Util;
        public IServiceProvider ServiceProvider;
        
        public WebAppFactory()
        {
            InMemoryDb = Guid.NewGuid().ToString();
        }
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<CaContext>(options =>
                {
                    options.UseInMemoryDatabase(InMemoryDb);
                });

                var sp = services.BuildServiceProvider();
                ServiceProvider = sp;
                Util = new TestUtil(sp);

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<CaContext>();
                db.Database.EnsureCreated();
                
                var generalSeeder = scope.ServiceProvider.GetRequiredService<GeneralModuleSeeder>();
                generalSeeder.Run().Wait();
            });
        }
        
        protected override IHostBuilder CreateHostBuilder()
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(x =>
                {
                    x.UseStartup<FakeStartup>().UseTestServer();
                })
                .UseEnvironment("Testing");
            return builder;
        }
    }
}