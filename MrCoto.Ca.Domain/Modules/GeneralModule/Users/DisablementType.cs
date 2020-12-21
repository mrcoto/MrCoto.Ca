using System;
using MrCoto.Ca.Domain.Common.Entities;

namespace MrCoto.Ca.Domain.Modules.GeneralModule.Users
{
    public class DisablementType : Entity<long>, ISoftDeletable, ITimestampable
    {
        public string Description { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}