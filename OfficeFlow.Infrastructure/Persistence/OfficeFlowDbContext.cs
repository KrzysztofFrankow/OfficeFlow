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
        public DbSet<Domain.Entities.Role> Roles { get; set; }
        public DbSet<Domain.Entities.Absences> Absences { get; set; }
        public DbSet<Domain.Entities.Dictionaries> Dictionaries { get; set; }
        public DbSet<Domain.Entities.Limits> Limits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Domain.Entities.Users>(e =>
            {
                e.HasKey(u => u.Id);
                e.OwnsOne(u => u.Address);
                e.Property(u => u.Email).IsRequired();
                e.Property(u => u.PasswordHash).IsRequired();
                e.Property(u => u.FirstName).IsRequired();
                e.Property(u => u.LastName).IsRequired();
            });

            modelBuilder.Entity<Domain.Entities.Role>(e =>
            {
                e.Property(u => u.Name).IsRequired();
            });

            modelBuilder.Entity<Domain.Entities.EFiles>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PublicId).HasDefaultValueSql("NEWID()");

                entity.HasOne(e => e.User)
                      .WithOne(u => u.EFile)
                      .HasForeignKey<Domain.Entities.EFiles>(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.EFileDocuments)
                      .WithOne(d => d.EFile)
                      .HasForeignKey(d => d.EFileId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<EFileDocuments>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<Domain.Entities.Absences>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PublicId).HasDefaultValueSql("NEWID()");

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Absences)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Domain.Entities.Limits>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PublicId).HasDefaultValueSql("NEWID()");

                entity.HasOne(e => e.User)
                      .WithMany(u => u.Limits)
                      .HasForeignKey(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}
