using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using pq_api.data.Entities;

namespace pq_api.data.Repositories
{
    public interface IQuestionCategoryRepository
    {
       
        IEnumerable<QuestionCategory> GetQuestionCategories(string userId, int id);
        IEnumerable<QuestionCategory> GetCategoriesForQuestion(string userId, int QuestionId);
        QuestionCategory Add(QuestionCategory entity);
        QuestionCategory DeleteQuestionCategory(string userId, int QuestionCategoryId);        

    }
}
