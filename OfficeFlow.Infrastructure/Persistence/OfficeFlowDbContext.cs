using Microsoft.EntityFrameworkCore;
using OfficeFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeFlow.Infrastructure.Persistence
{
    public class OfficeFlowDbContext : DbContext
    {
        public OfficeFlowDbContext(DbContextOptions<OfficeFlowDbContext> options) : base(options)
        {
            
        }
        public DbSet<Domain.Entities.Users> Users { get; set; }
        public DbSet<Domain.Entities.EFiles> EFiles { get; set; }
        public DbSet<Domain.Entities.EFileDocuments> EFileDocuments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Domain.Entities.Users>(e =>
            {
                e.HasKey(u => u.Id);
                e.OwnsOne(u => u.Address);
            });

            modelBuilder.Entity<Domain.Entities.EFiles>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PublicId).HasDefaultValueSql("NEWID()");

                entity.HasOne(e => e.User)
                      .WithOne(u => u.EFile)
                      .HasForeignKey<Domain.Entities.EFiles>(e => e.UserId);

                entity.HasMany(e => e.EFileDocuments)
                      .WithOne(d => d.EFile)
                      .HasForeignKey(d => d.EFileId);
            });

            modelBuilder.Entity<EFileDocuments>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }

    }
}
