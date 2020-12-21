using System;

namespace MrCoto.Ca.Domain.Common.Entities
{
    public interface ITimestampable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}