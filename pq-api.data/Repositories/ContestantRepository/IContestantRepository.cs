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
        IEnumerable<Contestant> GetContestants();
        IEnumerable<Contestant> GetContestantsForCompetition(string userId, int CompetitionId);
        Contestant GetByName(string userId, string name);
        Contestant Get(string userId, int id);
        Contestant Add(Contestant entity);
        Contestant Update(Contestant entity);
        Contestant Delete(string userId, int id);

    }
}
