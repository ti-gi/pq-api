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

        public IEnumerable<Quiz> All()
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
        //public IEnumerable<Round> GetRoundsForQuiz(int QuizId)
        //{
        //    return pqEntities.Rounds.Where(q => q.Quiz_ID_FK == QuizId).ToList();
        //}
        //#endregion

        //#region  QuizResults

        //public IEnumerable<QuizResult> GetQuizResults(int QuizId)
        //{
        //    return pqEntities.QuizResults.Where(qr => qr.Quiz_ID_FK == QuizId).ToList();
        //}

        //public IEnumerable<QuizResult> RefreshQuizResults(int QuizId)
        //{
        //    return pqEntities.Refresh_QuizResults(QuizId);
        //}

        //public QuizResult AddQuizResult(QuizResult entity)
        //{
        //    pqEntities.QuizResults.Add(entity);
        //    pqEntities.SaveChanges();
        //    return entity;
        //}

        //public QuizResult UpdateQuizResult(QuizResult entity)
        //{
        //    var existingQuizResult = pqEntities.QuizResults.Where(q => q.QuizResult_ID_PK == entity.QuizResult_ID_PK).FirstOrDefault();
        //    if (existingQuizResult != null)
        //    {
        //        existingQuizResult.Round_1_Points = entity.Round_1_Points;
        //        existingQuizResult.PointsAfterRound_1 = entity.PointsAfterRound_1;

        //        existingQuizResult.Round_2_Points = entity.Round_2_Points;
        //        existingQuizResult.PointsAfterRound_2 = entity.PointsAfterRound_2;

        //        existingQuizResult.Round_3_Points = entity.Round_3_Points;
        //        existingQuizResult.PointsAfterRound_3 = entity.PointsAfterRound_3;

        //        existingQuizResult.Round_4_Points = entity.Round_4_Points;
        //        existingQuizResult.PointsAfterRound_4 = entity.PointsAfterRound_4;
        //    }
        //    pqEntities.SaveChanges();
        //    return existingQuizResult;
        //}

        public IEnumerable<QuizResultFinal> QuizResultsFinal(int QuizId)
        {
            return pqEntities.QuizResultFinal.FromSqlRaw("Get_QuizResults @p0", QuizId).ToList();
        }


        //#endregion

    }


}