using Microsoft.EntityFrameworkCore;
using MrCoto.Ca.Domain.Modules.GeneralModule.Users;
using MrCoto.Ca.Infrastructure.Common.Repositories;

namespace MrCoto.Ca.Infrastructure.Modules.GeneralModule.Configuration
{
    public static class GeneralModelBuilder
    {
        public static ModelBuilder AddGeneral(this ModelBuilder builder)
        {
            
            builder.Entity<DisablementType>(entity => entity.BaseConfiguration("g_disablement_type"));
            
            builder.Entity<LoginMaxAttempt>(entity => entity.BaseConfiguration("g_login_max_attempt"));
            
            builder.Entity<PasswordReset>(entity => entity.BaseConfiguration("g_password_reset"));
            
            builder.Entity<RefreshToken>(entity =>
            {
                entity.BaseConfiguration("g_refresh_token");
                entity.Property(x => x.UserId).HasColumnName("g_user_id");
                entity.Property(x => x.TenantId).HasColumnName("g_tenant_id");
            });
            
            builder.Entity<UserDisablement>(entity =>
            {
                entity.BaseConfiguration("g_user_disablement");
                entity.Property(x => x.DisablementTypeId).HasColumnName("g_disablement_type_id");
                entity.Property(x => x.UserId).HasColumnName("g_user_id");
                entity.Property(x => x.AuthUserId).HasColumnName("g_auth_user_id");
            });
            
            builder.Entity<User>(entity =>
            {
                entity.BaseConfiguration("g_user");
                entity.Property(x => x.RoleId).HasColumnName("g_role_id");
                entity.Property(x => x.TenantId).HasColumnName("g_tenant_id");
            });
            
            builder.Entity<Role>(entity => entity.BaseConfiguration("g_role"));
            
            builder.Entity<Tenant>(entity => entity.BaseConfiguration("g_tenant"));
            
            builder.Entity<UserLogin>(entity =>
            {
                entity.BaseConfiguration("g_user_login");
                entity.Property(x => x.UserId).HasColumnName("g_user_id");
            });

            return builder;
        }

    }
}