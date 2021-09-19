using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using pq_api.data.Entities;

namespace pq_api.data.Repositories
{
    public class ContestantRepository : IContestantRepository
    {
        protected readonly pqsightcom_dev_core_1Context pqEntities;

        public ContestantRepository(pqsightcom_dev_core_1Context Entities)
        {
            pqEntities = Entities;
        }

        public Contestant Add(Contestant entity)
        {
            pqEntities.Contestants.Add(entity);
            pqEntities.SaveChanges();
            return entity;
        }

        public IEnumerable<Contestant> All()
        {
            return pqEntities.Contestants.ToList();
        }

        public IEnumerable<Contestant> Find(Expression<Func<Contestant, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Contestant Get(int id)
        {
            return pqEntities.Contestants.Where(q => q.ContestantIdPk == id).First();
        }

        public Contestant GetByName(string name)
        {
            return pqEntities.Contestants.Where(q => q.Name == name).First();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Contestant Update(Contestant entity)
        {
            var existingContestant = pqEntities.Contestants.Where(q => q.ContestantIdPk == entity.ContestantIdPk).FirstOrDefault();
            if (existingContestant != null)
            {
                existingContestant.Name = entity.Name;
            }
            pqEntities.SaveChanges();
            return existingContestant;
        }

        public IEnumerable<Contestant> GetContestantsForCompetition(int CompetitionId)
        {
            return pqEntities.Contestants.Where(q => q.CompetitionIdFk == CompetitionId).ToList();
        }
    }
}