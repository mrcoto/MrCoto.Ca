using System;

namespace MrCoto.Ca.Domain.Common.Entities
{
    public interface ISoftDeletable
    {
        public DateTime? DeletedAt { get; set; }
    }
}