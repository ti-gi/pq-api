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

        public IEnumerable<Quiz> GetQuizzesForCompetition(int CompetitionId)
        {
            return pqEntities.Quizzes.Where(q => q.CompetitionIdFk == CompetitionId).ToList();
        }

        public Quiz Add(Quiz entity)
        {
            pqEntities.Quizzes.Add(entity);
            pqEntities.SaveChanges();
            return entity;
        }

        public Quiz Update(Quiz entity)
        {
            var existingQuiz = pqEntities.Quizzes.Where(q => q.QuizIdPk == entity.QuizIdPk).FirstOrDefault();
            if (existingQuiz != null)
            {
                existingQuiz.Name = entity.Name;
            }
            pqEntities.SaveChanges();
            return existingQuiz;
        }

        public IEnumerable<Quiz> All(string userId)
        {
            return pqEntities.Quizzes.ToList();
        }

        public IEnumerable<Quiz> Find(Expression<Func<Quiz, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Quiz Get(int id)
        {
            return pqEntities.Quizzes.Where(q => q.QuizIdPk == id).First();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }



        #endregion

        //#region Round
        public IEnumerable<Round> GetRoundsForQuiz(int QuizId)
        {
            return pqEntities.Rounds.Where(q => q.QuizIdFk == QuizId).ToList();
        }
        //#endregion

        //#region  QuizResults

        public IEnumerable<QuizResult> GetQuizResults(int QuizId)
        {
            return pqEntities.QuizResults.Include( q => q.ContestantIdFkNavigation).Where(qr => qr.QuizIdFk == QuizId).ToList();
        }

        //public IEnumerable<QuizResult> RefreshQuizResults(int QuizId)
        //{
        //    return pqEntities.Refresh_QuizResults(QuizId);
        //}

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

        public QuizResult DeleteQuizResult(int id)
        {
            var quizResult = pqEntities.QuizResults.Where(r => r.QuizResultIdPk == id).First();
            pqEntities.QuizResults.Remove(quizResult);
            pqEntities.SaveChanges();
            return quizResult;
        }

        public IEnumerable<QuizResultFinal> QuizResultsFinal(int QuizId)
        {
            return pqEntities.QuizResultFinal.FromSqlRaw("Get_QuizResults @p0", QuizId).ToList();
        }


        //#endregion

    }


}