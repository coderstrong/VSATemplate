using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.VSATemplate.Domain.Entities;

namespace Org.VSATemplate.Infrastructure.Database.Configurations
{
    internal class ClassEntityConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.HasKey(m => m.Id);
        }
    }
}