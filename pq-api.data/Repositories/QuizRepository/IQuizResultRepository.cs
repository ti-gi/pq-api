using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using pq_api.data.Entities;

namespace pq_api.data.Repositories
{
    public interface IQuizResultRepository
    {
        IEnumerable<QuizResult> GetQuizResults(string userId);
        IEnumerable<QuizResult> GetQuizResults(string userId, int QuizId);
        QuizResult AddQuizResult(QuizResult Quiz);
        QuizResult UpdateQuizResult(QuizResult Quiz);
        QuizResult DeleteQuizResult(string userId, int id);
    }
}
