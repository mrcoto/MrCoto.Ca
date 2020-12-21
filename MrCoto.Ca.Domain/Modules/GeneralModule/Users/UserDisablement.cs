using System;
using MrCoto.Ca.Domain.Common.Entities;

namespace MrCoto.Ca.Domain.Modules.GeneralModule.Users
{
    public class UserDisablement : Entity<long>, ISoftDeletable, ITimestampable
    {
        public virtual long UserId { get; set; }
        public virtual User User { get; set; }
        public virtual long DisablementTypeId { get; set; }
        public virtual DisablementType DisablementType { get; set; }
        public string Observation { get; set; }
        public virtual long AuthUserId { get; set; }
        public virtual User AuthUser { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}