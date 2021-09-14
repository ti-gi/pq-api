using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace pq_api.data.Models
{
    public partial class pqsightcom_dev_core_1Context : DbContext
    {
        public pqsightcom_dev_core_1Context()
        {
        }

        public pqsightcom_dev_core_1Context(DbContextOptions<pqsightcom_dev_core_1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Competition> Competitions { get; set; }
        public virtual DbSet<Contestant> Contestants { get; set; }

        public virtual DbSet<CompetitionResults> CompetitionResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=mssql8.mojsite.com,1555;database=pqsightcom_dev_core_1;uid=pqsightcom_tomislav;pwd=#oo!SP1024");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("pqsightcom_tomislav")
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Competition>(entity =>
            {
                entity.HasKey(e => e.CompetitionIdPk);

                entity.ToTable("Competitions", "dbo");

                entity.Property(e => e.CompetitionIdPk).HasColumnName("Competition_ID_PK");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Contestant>(entity =>
            {
                entity.HasKey(e => e.ContestantIdPk);

                entity.ToTable("Contestants", "dbo");

                entity.Property(e => e.ContestantIdPk).HasColumnName("Contestant_ID_PK");

                entity.Property(e => e.CompetitionIdFk).HasColumnName("Competition_ID_FK");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.CompetitionIdFkNavigation)
                    .WithMany(p => p.Contestants)
                    .HasForeignKey(d => d.CompetitionIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contestants_Competitions");
            });

            modelBuilder.Entity<CompetitionResults>(entity =>
            {
                entity.HasKey(e => e.Name);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
