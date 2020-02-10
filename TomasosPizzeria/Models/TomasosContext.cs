﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TomasosPizzeria.Models
{
    public partial class TomasosContext : DbContext
    {
        public TomasosContext()
        {
        }

        public TomasosContext(DbContextOptions<TomasosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Bestallning> Bestallning { get; set; }
        public virtual DbSet<BestallningMatratt> BestallningMatratt { get; set; }
        public virtual DbSet<Kund> Kund { get; set; }
        public virtual DbSet<Matratt> Matratt { get; set; }
        public virtual DbSet<MatrattProdukt> MatrattProdukt { get; set; }
        public virtual DbSet<MatrattTyp> MatrattTyp { get; set; }
        public virtual DbSet<Produkt> Produkt { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost; Database=Tomasos ;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Bestallning>(entity =>
            {
                entity.Property(e => e.BestallningId).HasColumnName("BestallningID");

                entity.Property(e => e.BestallningDatum).HasColumnType("datetime");

                entity.Property(e => e.KundId)
                    .IsRequired()
                    .HasColumnName("KundID")
                    .HasMaxLength(450);

                entity.HasOne(d => d.Kund)
                    .WithMany(p => p.Bestallning)
                    .HasForeignKey(d => d.KundId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Bestallning_Kund");
            });

            modelBuilder.Entity<BestallningMatratt>(entity =>
            {
                entity.HasKey(e => new { e.MatrattId, e.BestallningId });

                entity.Property(e => e.MatrattId).HasColumnName("MatrattID");

                entity.Property(e => e.BestallningId).HasColumnName("BestallningID");

                entity.Property(e => e.Antal).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Bestallning)
                    .WithMany(p => p.BestallningMatratt)
                    .HasForeignKey(d => d.BestallningId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BestallningMatratt_Bestallning");

                entity.HasOne(d => d.Matratt)
                    .WithMany(p => p.BestallningMatratt)
                    .HasForeignKey(d => d.MatrattId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BestallningMatratt_Matratt");
            });

            modelBuilder.Entity<Kund>(entity =>
            {
                entity.Property(e => e.KundId).HasColumnName("KundID");

                entity.Property(e => e.AnvandarNamn)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gatuadress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Losenord)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Namn)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Postnr)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Postort)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefon)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Matratt>(entity =>
            {
                entity.Property(e => e.MatrattId).HasColumnName("MatrattID");

                entity.Property(e => e.Beskrivning)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MatrattNamn)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MatrattTypNavigation)
                    .WithMany(p => p.Matratt)
                    .HasForeignKey(d => d.MatrattTyp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Matratt_MatrattTyp");
            });

            modelBuilder.Entity<MatrattProdukt>(entity =>
            {
                entity.HasKey(e => new { e.MatrattId, e.ProduktId });

                entity.Property(e => e.MatrattId).HasColumnName("MatrattID");

                entity.Property(e => e.ProduktId).HasColumnName("ProduktID");

                entity.HasOne(d => d.Matratt)
                    .WithMany(p => p.MatrattProdukt)
                    .HasForeignKey(d => d.MatrattId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatrattProdukt_Matratt");

                entity.HasOne(d => d.Produkt)
                    .WithMany(p => p.MatrattProdukt)
                    .HasForeignKey(d => d.ProduktId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MatrattProdukt_Produkt");
            });

            modelBuilder.Entity<MatrattTyp>(entity =>
            {
                entity.HasKey(e => e.MatrattTyp1);

                entity.Property(e => e.MatrattTyp1).HasColumnName("MatrattTyp");

                entity.Property(e => e.Beskrivning)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Produkt>(entity =>
            {
                entity.Property(e => e.ProduktId).HasColumnName("ProduktID");

                entity.Property(e => e.ProduktNamn)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
