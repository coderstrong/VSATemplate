using MakeSimple.SharedKernel.Contract;
using Microsoft.EntityFrameworkCore;
using Org.VSATemplate.Domain.Entities;
using Org.VSATemplate.Infrastructure.Database.Configurations;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Org.VSATemplate.Infrastructure.Database
{
    public class CoreDBContext : DbContext, IUnitOfWork
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Class> Classes { get; set; }

        public string Uuid => Guid.NewGuid().ToString();

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            return (await SaveChangesAsync(cancellationToken)) > 0;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("ForTest");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ClassEntityConfiguration());
        }

        //entities
    }
}
