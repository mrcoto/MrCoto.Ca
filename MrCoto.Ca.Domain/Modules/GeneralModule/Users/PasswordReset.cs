using System;
using MrCoto.Ca.Domain.Common.Entities;

namespace MrCoto.Ca.Domain.Modules.GeneralModule.Users
{
    public class PasswordReset : Entity<long>, ITimestampable
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}