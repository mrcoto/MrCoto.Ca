using System;
using MrCoto.Ca.Domain.Common.Entities;

namespace MrCoto.Ca.Domain.Modules.GeneralModule.Users
{
    public class Role : Entity<long>, ISoftDeletable
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}