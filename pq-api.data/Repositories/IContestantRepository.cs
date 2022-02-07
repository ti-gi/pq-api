using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using pq_api.data.Entities;

namespace pq_api.data.Repositories
{
    public interface IContestantRepository
    {
        Contestant Add(Contestant entity);
        Contestant Update(Contestant entity);
        Contestant Delete(int id);
        Contestant Get(int id);
        Contestant GetByName(string name);
        IEnumerable<Contestant> All();
        IEnumerable<Contestant> Find(Expression<Func<Contestant, bool>> predicate);
        void SaveChanges();

        IEnumerable<Contestant> GetContestantsForCompetition(int CompetitionId);


    }
}
