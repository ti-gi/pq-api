using pq_api.data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pq_api.data.Repositories
{
    public interface IStatisticsRepository
    {
        IEnumerable<CategoryCount> GetCategoriesCount(string userId);

        IEnumerable<DifficultyCount> GetDifficultyCount(string userId);
    }
}
