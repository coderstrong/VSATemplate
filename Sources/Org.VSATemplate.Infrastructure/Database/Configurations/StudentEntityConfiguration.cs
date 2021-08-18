using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Org.VSATemplate.Domain.Entities;

namespace Org.VSATemplate.Infrastructure.Database.Configurations
{
    internal class StudentEntityConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasMany(e => e.Classes).WithOne(e => e.Student).HasForeignKey(e => e.StudentId);
        }
    }
}