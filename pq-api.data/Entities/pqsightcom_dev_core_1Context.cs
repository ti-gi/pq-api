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

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Competition> Competitions { get; set; }
        public virtual DbSet<CompetitionCategoryCount> CompetitionCategoryCounts { get; set; }
        public virtual DbSet<Contestant> Contestants { get; set; }
        public virtual DbSet<ContestantWins> ContestantWins { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionCategory> QuestionCategories { get; set; }
        public virtual DbSet<Quiz> Quizzes { get; set; }
        public virtual DbSet<QuizResult> QuizResults { get; set; }
        public virtual DbSet<Round> Rounds { get; set; }
        public virtual DbSet<CompetitionResults> CompetitionResults { get; set; }
        public virtual DbSet<QuizResultFinal> QuizResultFinal { get; set; }

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

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryIdPk)
                    .HasName("Category_ID_PK");

                entity.ToTable("Categories", "dbo");

                entity.Property(e => e.CategoryIdPk).HasColumnName("Category_ID_PK");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Competition>(entity =>
            {
                entity.HasKey(e => e.CompetitionIdPk);

                entity.ToTable("Competitions", "dbo");

                entity.Property(e => e.CompetitionIdPk).HasColumnName("Competition_ID_PK");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CompetitionCategoryCount>(entity =>
            {
                entity.HasNoKey();
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

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.CompetitionIdFkNavigation)
                    .WithMany(p => p.Contestants)
                    .HasForeignKey(d => d.CompetitionIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contestants_Competitions");
            });

            modelBuilder.Entity<ContestantWins>(entity =>
            {
                entity.HasKey(e => e.Name);
            });

            modelBuilder.Entity<CompetitionResults>(entity =>
            {
                entity.HasKey(e => e.Name);
            });

            modelBuilder.Entity<QuizResultFinal>(entity =>
            {
                entity.HasKey(e => e.Name);
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.QuestionIdPk);

                entity.ToTable("Questions", "dbo");

                entity.Property(e => e.QuestionIdPk).HasColumnName("Question_ID_PK");

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Question1)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("Question");

                entity.Property(e => e.RoundIdFk).HasColumnName("Round_ID_FK");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.RoundIdFkNavigation)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.RoundIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Questions_Rounds");
            });

            modelBuilder.Entity<QuestionCategory>(entity =>
            {
                entity.HasKey(e => e.QuestionCategoryIdPk)
                    .HasName("QuestionCategory_ID_PK");

                entity.ToTable("QuestionCategories", "dbo");

                entity.Property(e => e.QuestionCategoryIdPk).HasColumnName("QuestionCategory_ID_PK");

                entity.Property(e => e.CategoryIdFk).HasColumnName("Category_ID_FK");

                entity.Property(e => e.QuestionIdFk).HasColumnName("Question_ID_FK");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.CategoryIdFkNavigation)
                    .WithMany(p => p.QuestionCategories)
                    .HasForeignKey(d => d.CategoryIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionCategories_Categories");

                entity.HasOne(d => d.QuestionIdFkNavigation)
                    .WithMany(p => p.QuestionCategories)
                    .HasForeignKey(d => d.QuestionIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_QuestionCategories_Questions");
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

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(100);

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

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(100);

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

            modelBuilder.Entity<Round>(entity =>
            {
                entity.HasKey(e => e.RoundIdPk);

                entity.ToTable("Rounds", "dbo");

                entity.Property(e => e.RoundIdPk).HasColumnName("Round_ID_PK");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.QuizIdFk).HasColumnName("Quiz_ID_FK");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.QuizIdFkNavigation)
                    .WithMany(p => p.Rounds)
                    .HasForeignKey(d => d.QuizIdFk)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Rounds_Quizzes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
