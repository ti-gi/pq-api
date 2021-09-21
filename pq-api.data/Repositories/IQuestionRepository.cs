using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using pq_api.data.Entities;

namespace pq_api.data.Repositories
{
    public interface IQuestionRepository
    {
        Question Add(Question entity);
        Question Update(Question entity);
        Question Get(int id);
        IEnumerable<Question> All();
        IEnumerable<Question> Find(Expression<Func<Question, bool>> predicate);
        void SaveChanges();

        QuestionCategory Add(QuestionCategory entity);
        QuestionCategory Delete(int QuestionCategoryId);
        Category Add(Category entity);
        IEnumerable<QuestionCategory> GetCategoriesForQuestion(int QuestionId);

        IEnumerable<Question> GetQuestionsForRound(int RoundId);
        IEnumerable<Category> GetCategories();
        IEnumerable<QuestionCategory> GetQuestionCategories(int id);

    }
}
