using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AnDhanBkngAPI.Models
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

        public virtual DbSet<AnDhanBkng> AnDhanBkngs { get; set; } = null!;
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
            modelBuilder.Entity<AnDhanBkng>(entity =>
            {
                entity.HasKey(e => e.Bkid)
                    .HasName("PK__AnDhanBk__51399146DF2C8083");

                entity.ToTable("AnDhanBkng");

                entity.Property(e => e.Bkid).HasColumnName("bkid");

                entity.Property(e => e.Cost).HasColumnName("cost");

                entity.Property(e => e.Det)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("det");

                entity.Property(e => e.Edt)
                    .HasColumnType("datetime")
                    .HasColumnName("edt");

                entity.Property(e => e.Sdt)
                    .HasColumnType("datetime")
                    .HasColumnName("sdt");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AnDhanBkngs)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK__AnDhanBkn__useri__6A30C649");
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
