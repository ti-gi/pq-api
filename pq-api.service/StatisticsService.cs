using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using pq_api.data.Repositories;
using pq_api.service.BusinessModels;
using B = pq_api.service.BusinessModels;
using E = pq_api.data.Entities;


namespace pq_api.service
{
    public class StatisticsService : IStatisticsService
    {
        private IStatisticsRepository statisticsRepository;

        public StatisticsService(IStatisticsRepository statisticsRepository)
        {
            this.statisticsRepository = statisticsRepository;
        }

        public IEnumerable<B.CategoryCount> GetCategoriesCount(string userId)
        {
            IEnumerable<B.CategoryCount> rtn = statisticsRepository.GetCategoriesCount(userId).Select(c => new B.CategoryCount
            {
                Category = c.Category,
                Count = c.Count
            });

            return rtn;
        }
    }
}