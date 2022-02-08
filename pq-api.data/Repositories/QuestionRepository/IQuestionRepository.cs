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
        IEnumerable<Question> GetQuestions();
        IEnumerable<Question> GetQuestionsForRound(int RoundId);
        Question Get(int id);
        Question Add(Question entity);
        Question Update(Question entity);


        IEnumerable<QuestionCategory> GetQuestionCategories(int id);
        IEnumerable<QuestionCategory> GetCategoriesForQuestion(int QuestionId);
        QuestionCategory Add(QuestionCategory entity);
        QuestionCategory Delete(int QuestionCategoryId);

        
        IEnumerable<Category> GetCategories();
        Category Add(Category entity);
        

    }
}
