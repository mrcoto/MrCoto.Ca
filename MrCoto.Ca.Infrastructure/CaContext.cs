using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using MrCoto.Ca.Infrastructure.Modules.GeneralModule.Configuration;

namespace MrCoto.Ca.Infrastructure
{
    public class CaContext : DbContext
    {
        private readonly IHostEnvironment _environment;

        public CaContext(DbContextOptions<CaContext> options, IHostEnvironment environment)
            : base(options)
        {
            _environment = environment;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseLazyLoadingProxies();
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.AddGeneral();

            if (_environment.EnvironmentName.Equals("Testing"))
            {
                builder.Entity<UserLogin>(entity =>
                {
                    entity.Property(x => x.ClientIp).HasConversion(
                        v => v.MapToIPv4().ToString(),
                        v => IPAddress.Parse(v)
                    );
                });
            }
        }

        /**
         * GENERAL MODULE
         */
        public DbSet<DisablementType> DisablementTypes { get; set; }
        public DbSet<LoginMaxAttempt> LoginMaxAttempts { get; set; }
        public DbSet<PasswordReset> PasswordResets { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserDisablement> UserDisablements { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        
    }
}