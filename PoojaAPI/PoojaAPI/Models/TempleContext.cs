using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PoojaAPI.Models
{
    public partial class TempleContext : DbContext
    {
        public TempleContext()
        {
        }

        public TempleContext(DbContextOptions<TempleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pooja> Poojas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=Temple;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pooja>(entity =>
            {
                entity.HasKey(e => e.Pid)
                    .HasName("PK__Pooja__DD37D91A97661682");

                entity.ToTable("Pooja");

                entity.Property(e => e.Pid).HasColumnName("pid");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Details)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("details");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
