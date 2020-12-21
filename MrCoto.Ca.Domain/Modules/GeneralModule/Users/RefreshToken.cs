using System;
using MrCoto.Ca.Domain.Common.Entities;

namespace MrCoto.Ca.Domain.Modules.GeneralModule.Users
{
    public class RefreshToken : Entity<long>, ITenantable, ISoftDeletable, ITimestampable
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public virtual long UserId { get; set; }
        public virtual User User { get; set; }
        public long TenantId { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public bool IsExpired() => ExpiresAt < DateTime.Now;
    }
}