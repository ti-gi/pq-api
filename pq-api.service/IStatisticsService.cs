using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using B = pq_api.service.BusinessModels;

namespace pq_api.service
{
    public interface IStatisticsService
    {
        IEnumerable<B.CategoryCount> GetCategoriesCount(string userId);
    }
}
