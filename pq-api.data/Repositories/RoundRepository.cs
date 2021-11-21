using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using pq_api.data.Entities;

namespace pq_api.data.Repositories
{
    public class RoundRepository : IRoundRepository
    {
        protected readonly pqsightcom_dev_core_1Context pqEntities;
        public RoundRepository(pqsightcom_dev_core_1Context entities)
        {
            pqEntities = entities;
        }

        public IEnumerable<Round> All()
        {
            return pqEntities.Rounds.ToList();
        }

        public Round Get(int id)
        {
            return pqEntities.Rounds.Where(q => q.RoundIdPk == id).First();
        }


        public Round Add(Round entity)
        {
            entity.RoundNumber = CountOfRoundsPerQuiz(entity.QuizIdFk) + 1;
            pqEntities.Rounds.Add(entity);
            pqEntities.SaveChanges();
            return entity;
        }

        public Round Update(Round entity)
        {
            var existingRound = pqEntities.Rounds.Where(q => q.RoundIdPk == entity.RoundIdPk).FirstOrDefault();
            if (existingRound != null)
            {
                existingRound.Name = entity.Name;
            }
            pqEntities.SaveChanges();
            return existingRound;
        }

        //public RoundResult GetRoundResult(int RoundResultId)
        //{
        //    return pqEntities.RoundResults.Where(r => r.RoundResult_ID_PK == RoundResultId).First();
        //}

        //public RoundResult AddRoundResult(RoundResult entity)
        //{
        //    pqEntities.RoundResults.Add(entity);
        //    pqEntities.SaveChanges();
        //    return entity;
        //}

        //public RoundResult DeleteRoundResult(int RoundResultId)
        //{
        //    var roundResult = pqEntities.RoundResults.Where(r => r.RoundResult_ID_PK == RoundResultId).First();
        //    pqEntities.RoundResults.Remove(roundResult);
        //    pqEntities.SaveChanges();
        //    return roundResult;
        //}

        //public IEnumerable<RoundResult> GetRoundResultsForRound(int RoundId)
        //{
        //    return pqEntities.RoundResults.Where(r => r.Round_ID_FK == RoundId).ToList();
        //}

        //public RoundResult UpdateRoundResult(RoundResult entity)
        //{
        //    var existingRoundResult = pqEntities.RoundResults.Where(q => q.RoundResult_ID_PK == entity.RoundResult_ID_PK).FirstOrDefault();
        //    if (existingRoundResult != null)
        //    {
        //        existingRoundResult.Contestant_ID_FK = entity.Contestant_ID_FK;
        //        existingRoundResult.Points1 = entity.Points1;
        //        existingRoundResult.Points2 = entity.Points2;
        //        existingRoundResult.Points3 = entity.Points3;
        //    }
        //    pqEntities.SaveChanges();
        //    return existingRoundResult;
        //}

        public int CountOfRoundsPerQuiz(int QuizId)
        {
            return pqEntities.Rounds.Where(r => r.QuizIdFk == QuizId).Count();
        }

        public IEnumerable<Round> Find(Expression<Func<Round, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}