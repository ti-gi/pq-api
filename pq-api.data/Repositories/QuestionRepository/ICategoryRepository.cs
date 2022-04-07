using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using pq_api.data.Entities;

namespace pq_api.data.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetCategories(string userId);
        IEnumerable<CompetitionCategoryCount> GetCategoriesForCompetitionCount(string userId, int competitionId);
        Category Add(Category entity);
        
    }
}
