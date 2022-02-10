using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Microsoft.EntityFrameworkCore;
using pq_api.data.Entities;

namespace pq_api.data.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        protected readonly pqsightcom_dev_core_1Context pqEntities;

        public QuizRepository(pqsightcom_dev_core_1Context Entities)
        {
            pqEntities = Entities;
        }

        #region Quiz
        public IEnumerable<Quiz> GetQuizzes(string userId)
        {
            return pqEntities.Quizzes.Where(q => q.UserId == userId).ToList();
        }

        public IEnumerable<Quiz> GetQuizzesForCompetition(string userId, int CompetitionId)
        {
            return pqEntities.Quizzes.Where(q => q.UserId == userId && q.CompetitionIdFk == CompetitionId).ToList();
        }

        public Quiz Get(string userId, int id)
        {
            return pqEntities.Quizzes.Where(q => q.UserId == userId && q.QuizIdPk == id).First();
        }

        public Quiz Add(Quiz entity)
        {
            pqEntities.Quizzes.Add(entity);
            pqEntities.SaveChanges();
            return entity;
        }

        public Quiz Update(Quiz entity)
        {
            var existingQuiz = pqEntities.Quizzes.Where(q => q.UserId == entity.UserId && q.QuizIdPk == entity.QuizIdPk).FirstOrDefault();
            if (existingQuiz != null)
            {
                existingQuiz.Name = entity.Name;
            }
            pqEntities.SaveChanges();
            return existingQuiz;
        }

        #endregion

        #region  QuizResults
        public IEnumerable<QuizResult> GetQuizResults(string userId)
        {
            return pqEntities.QuizResults.Include(q => q.ContestantIdFkNavigation).Where(q => q.UserId == userId).ToList();
        }

        public IEnumerable<QuizResult> GetQuizResults(string userId, int QuizId)
        {
            return pqEntities.QuizResults.Include( q => q.ContestantIdFkNavigation).Where(qr => qr.UserId == userId && qr.QuizIdFk == QuizId).ToList();
        }

        public QuizResult AddQuizResult(QuizResult entity)
        {
            pqEntities.QuizResults.Add(entity);
            pqEntities.SaveChanges();
            return entity;
        }

        public QuizResult UpdateQuizResult(QuizResult entity)
        {
            var existingQuizResult = pqEntities.QuizResults.Where(q => q.QuizResultIdPk == entity.QuizResultIdPk).FirstOrDefault();
            if (existingQuizResult != null)
            {
                existingQuizResult.Round1Points = entity.Round1Points;
                existingQuizResult.PointsAfterRound1 = entity.PointsAfterRound1;

                existingQuizResult.Round2Points = entity.Round2Points;
                existingQuizResult.PointsAfterRound2 = entity.PointsAfterRound2;

                existingQuizResult.Round3Points = entity.Round3Points;
                existingQuizResult.PointsAfterRound3 = entity.PointsAfterRound3;

                existingQuizResult.Round4Points = entity.Round4Points;
                existingQuizResult.PointsAfterRound4 = entity.PointsAfterRound4;
            }
            pqEntities.SaveChanges();
            return existingQuizResult;
        }

        public QuizResult DeleteQuizResult(string userId, int id)
        {
            var quizResult = pqEntities.QuizResults.Where(r => r.UserId == userId && r.QuizResultIdPk == id).First();
            pqEntities.QuizResults.Remove(quizResult);
            pqEntities.SaveChanges();
            return quizResult;
        }


        public IEnumerable<QuizResultFinal> QuizResultsFinal(int QuizId)
        {
            return pqEntities.QuizResultFinal.FromSqlRaw("Get_QuizResults @p0", QuizId).ToList();
        }

        #endregion

    }


}