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

        public IEnumerable<Round> All(string userId)
        {
            return pqEntities.Rounds.ToList();
        }

        public Round Get(string userId, int id)
        {
            return pqEntities.Rounds.Where(q => q.RoundIdPk == id).First();
        }

        public Round Add(Round entity)
        {
            entity.RoundNumber = CountOfRoundsPerQuiz(entity.UserId, entity.QuizIdFk) + 1;
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

        public int CountOfRoundsPerQuiz(string userId, int QuizId)
        {
            return pqEntities.Rounds.Where(r => r.QuizIdFk == QuizId).Count();
        }

    }
}