using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace pq_api.data.Entities
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
        public virtual DbSet<Quiz> Quizzes { get; set; }
        public virtual DbSet<QuizResult> QuizResults { get; set; }
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

            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.HasKey(e => e.QuizIdPk);

                entity.ToTable("Quizzes", "dbo");

                entity.Property(e => e.QuizIdPk).HasColumnName("Quiz_ID_PK");

                entity.Property(e => e.CompetitionIdFk).HasColumnName("Competition_ID_FK");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.CompetitionIdFkNavigation)
                    .WithMany(p => p.Quizzes)
                    .HasForeignKey(d => d.CompetitionIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Competition_Id_FK__Quiz_Id_PK");
            });

            modelBuilder.Entity<QuizResult>(entity =>
            {
                entity.HasKey(e => e.QuizResultIdPk);

                entity.ToTable("QuizResults", "dbo");

                entity.Property(e => e.QuizResultIdPk).HasColumnName("QuizResult_ID_PK");

                entity.Property(e => e.ContestantIdFk).HasColumnName("Contestant_ID_FK");

                entity.Property(e => e.PointsAfterRound1)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("PointsAfterRound_1");

                entity.Property(e => e.PointsAfterRound2)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("PointsAfterRound_2");

                entity.Property(e => e.PointsAfterRound3)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("PointsAfterRound_3");

                entity.Property(e => e.PointsAfterRound4)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("PointsAfterRound_4");

                entity.Property(e => e.QuizIdFk).HasColumnName("Quiz_ID_FK");

                entity.Property(e => e.Round1Points)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Round_1_Points");

                entity.Property(e => e.Round2Points)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Round_2_Points");

                entity.Property(e => e.Round3Points)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Round_3_Points");

                entity.Property(e => e.Round4Points)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("Round_4_Points");

                entity.Property(e => e.RoundResult1IdFk).HasColumnName("RoundResult_1_ID_FK");

                entity.Property(e => e.RoundResult2IdFk).HasColumnName("RoundResult_2_ID_FK");

                entity.Property(e => e.RoundResult3IdFk).HasColumnName("RoundResult_3_ID_FK");

                entity.Property(e => e.RoundResult4IdFk).HasColumnName("RoundResult_4_ID_FK");

                entity.HasOne(d => d.ContestantIdFkNavigation)
                    .WithMany(p => p.QuizResults)
                    .HasForeignKey(d => d.ContestantIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuizResults_Contestants");

                entity.HasOne(d => d.QuizIdFkNavigation)
                    .WithMany(p => p.QuizResults)
                    .HasForeignKey(d => d.QuizIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuizResults_Quiz");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
