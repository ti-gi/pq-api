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

        public IEnumerable<Contestant> GetContestants()
        {
            return pqEntities.Contestants.ToList();
        }

        public IEnumerable<Contestant> GetContestantsForCompetition(int CompetitionId)
        {
            return pqEntities.Contestants.Where(q => q.CompetitionIdFk == CompetitionId).ToList();
        }

        public Contestant GetByName(string name)
        {
            if (pqEntities.Contestants.Where(q => q.Name == name).Count() > 0)
                return pqEntities.Contestants.Where(q => q.Name == name).First();
            else
                return null;
        }

        public Contestant Get(int id)
        {
            return pqEntities.Contestants.Where(q => q.ContestantIdPk == id).First();
        }

        public Contestant Add(Contestant entity)
        {
            pqEntities.Contestants.Add(entity);
            pqEntities.SaveChanges();
            return entity;
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

        public Contestant Delete(int id)
        {
            var contestant = pqEntities.Contestants.Where(r => r.ContestantIdPk == id).First();
            pqEntities.Contestants.Remove(contestant);
            pqEntities.SaveChanges();
            return contestant;
        }

    }
}