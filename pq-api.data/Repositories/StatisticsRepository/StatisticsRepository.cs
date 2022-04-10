using Microsoft.EntityFrameworkCore;
using pq_api.data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pq_api.data.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        protected readonly pqsightcom_dev_core_1Context pqEntities;
        public StatisticsRepository(pqsightcom_dev_core_1Context entities)
        {
            pqEntities = entities;
        }
        public IEnumerable<CategoryCount> GetCategoriesCount(string userId)
        {
            return pqEntities.CategoryCounts.FromSqlRaw("Get_CategoriesCount @p0", userId).ToList();
        }
    }
}
