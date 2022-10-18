using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PoojaBkngAPI.Models
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
        public virtual DbSet<PoojaBkng> PoojaBkngs { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

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

            modelBuilder.Entity<PoojaBkng>(entity =>
            {
                entity.HasKey(e => e.Bkid)
                    .HasName("PK__PoojaBkn__513991461EA4CC81");

                entity.ToTable("PoojaBkng");

                entity.Property(e => e.Bkid).HasColumnName("bkid");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Det)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("det");

                entity.Property(e => e.Edt)
                    .HasColumnType("datetime")
                    .HasColumnName("edt");

                entity.Property(e => e.Pooid).HasColumnName("pooid");

                entity.Property(e => e.Sdt)
                    .HasColumnType("datetime")
                    .HasColumnName("sdt");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Poo)
                    .WithMany(p => p.PoojaBkngs)
                    .HasForeignKey(d => d.Pooid)
                    .HasConstraintName("FK__PoojaBkng__pooid__73BA3083");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PoojaBkngs)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__PoojaBkng__useri__74AE54BC");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__user___DD70126479306A8E");

                entity.ToTable("user_");

                entity.Property(e => e.Uid).HasColumnName("uid");

                entity.Property(e => e.Emailid)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("emailid");

                entity.Property(e => e.Pword)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pword");

                entity.Property(e => e.Uname)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("uname");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
