using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.VSATemplate.Domain.Entities;

namespace Org.VSATemplate.Infrastructure.Database.Configurations
{
    class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(m => m.Id);
        }
    }
}
