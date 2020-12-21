using System;
using System.Net;
using MrCoto.Ca.Domain.Common.Entities;

namespace MrCoto.Ca.Domain.Modules.GeneralModule.Users
{
    public class UserLogin : Entity<long>, ISoftDeletable, ITimestampable
    {
        public virtual long UserId { get; set; }
        public virtual User User { get; set; }
        public string Device { get; set; }
        public string DeviceType { get; set; }
        public string DeviceFamily { get; set; }
        public string Browser { get; set; }
        public string BrowserFamily { get; set; }
        public string UserAgent { get; set; }
        public IPAddress ClientIp { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public DateTime? DeletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}