using System;
using MrCoto.Ca.Domain.Common.Entities;

namespace MrCoto.Ca.Domain.Modules.GeneralModule.Users
{
    public class User : Entity<long>, ITenantable, ISoftDeletable, ITimestampable
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual long TenantId { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual long RoleId { get; set; }
        public virtual Role Role { get; set; }
        public int LoginAttempts { get; set; }
        public DateTime? DisabledAccountAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool HasDisabledAccount() => DisabledAccountAt != null;
        public bool HasEnabledAccount() => !HasDisabledAccount();

    }
}