using System;

namespace MrCoto.Ca.Application.Modules.GeneralModule.Users.Query.Default.Response
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public TenantDto Tenant { get; set; }
        public RoleDto Role { get; set; }
        public int LoginAttempts { get; set; }
        public DateTime? DisabledAccountAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}