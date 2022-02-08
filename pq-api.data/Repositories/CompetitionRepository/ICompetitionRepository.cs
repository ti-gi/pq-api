using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using pq_api.data.Entities;

namespace pq_api.data.Repositories
{
    public interface ICompetitionRepository
    {
        IEnumerable<Competition> GetCompetitions(string userId);
        Competition Get(string userId, int id);
        Competition Add(Competition entity);
        Competition Update(Competition entity);
        
        IEnumerable<CompetitionResults> CompetitionResults(int CompetitionId);
    }
}
