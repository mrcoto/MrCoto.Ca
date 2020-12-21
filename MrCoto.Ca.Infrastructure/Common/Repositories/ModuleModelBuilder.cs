using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MrCoto.Ca.Domain.Common.Entities;

namespace MrCoto.Ca.Infrastructure.Common.Repositories
{
    public static class ModuleModelBuilder
    {
        public static EntityTypeBuilder<T> BaseConfiguration<T>(this EntityTypeBuilder<T> builder, string tableName) where T : Entity<long>
        {
            builder.ToTable(tableName)
                .Ignore(entity => entity.DomainEvents)
                .HasKey(entity => entity.Id);

            return builder;
        }
    }
}