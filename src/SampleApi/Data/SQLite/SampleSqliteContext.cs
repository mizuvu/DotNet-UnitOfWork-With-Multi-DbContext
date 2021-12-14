using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SampleApi.Interfaces;

#nullable disable

namespace SampleApi.Data.SQLite
{
    public partial class SampleSqliteContext : DbContext, ISampleSqliteContext
    {
        public SampleSqliteContext()
        {
        }

        public SampleSqliteContext(DbContextOptions<SampleSqliteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.Id, "IX_Products_Id")
                    .IsUnique();

                entity.Property(e => e.IsBlocked)
                    .IsRequired()
                    .HasColumnType("BOOLEAN")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("NVARCHAR (450)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
