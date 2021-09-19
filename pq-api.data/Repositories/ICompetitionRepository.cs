using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using pq_api.data.Entities;

namespace pq_api.data.Repositories
{
    public interface ICompetitionRepository : IRepository<Competition>
    {
        IEnumerable<CompetitionResults> CompetitionResults(int CompetitionId);
    }
}
