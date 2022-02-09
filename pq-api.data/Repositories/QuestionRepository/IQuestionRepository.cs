using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using pq_api.data.Entities;

namespace pq_api.data.Repositories
{
    public interface IQuestionRepository: IQuestionCategoryRepository, ICategoryRepository
    {
        IEnumerable<Question> GetQuestions(string userId);
        IEnumerable<Question> GetQuestionsForRound(string userId, int roundId);
        Question Get(string userId, int id);
        Question Add(Question entity);
        Question Update(Question entity);

    }
}
