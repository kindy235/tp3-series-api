using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SeriesApi.Models.EntityFramework
{
    public partial class SeriesDbContext : DbContext
    {
        //public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        public SeriesDbContext()
        {
        }

        public SeriesDbContext(DbContextOptions<SeriesDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Serie> Series { get; set; }
        public virtual DbSet<Notation> Notations { get; set; }
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLoggerFactory(MyLoggerFactory)
                    .EnableSensitiveDataLogging()
                    .UseNpgsql("Server=localhost;port=5432;Database=seriesDB;uid=postgres;password=admin;");
                
                //optionsBuilder.UseLazyLoadingProxies();
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            
            modelBuilder.Entity<Notation>(entity =>
            {
                entity.HasKey(e => new { e.UtilisateurId, e.SerieId }).HasName("pk_not");

                entity.ToTable(b => b.HasCheckConstraint("ck_not_note", "not_note between 0 and 5"));

                entity.HasOne(d => d.UtilisateurNotant)
                    .WithMany(p => p.NotesUtilisateur)
                    .HasForeignKey(d => d.UtilisateurId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_not_utl");

                entity.HasOne(d => d.SerieNotee)
                    .WithMany(p => p.NotesSerie)
                    .HasForeignKey(d => d.SerieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_not_ser");
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.HasKey(e => e.UtilisateurId).HasName("pk_utl");

                entity.Property(e => e.Pays).HasDefaultValue("France");
                entity.Property(e => e.DateCreation).HasDefaultValueSql("now()");
                entity.Property(e => e.Mobile).HasColumnType("char(10)").IsFixedLength();

                entity.HasIndex(e => e.Mail)
                .IsUnique();
            });

            modelBuilder.Entity<Serie>(entity =>
            {
                entity.HasKey(e => e.SerieId).HasName("pk_ser");

                entity.Property(e => e.Titre).IsRequired().HasColumnType("varchar(100)");
                entity.Property(e => e.Network).HasColumnType("varchar(50)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
