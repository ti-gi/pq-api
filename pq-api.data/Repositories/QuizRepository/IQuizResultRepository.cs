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
        IEnumerable<QuizResult> GetQuizResults();
        IEnumerable<QuizResult> GetQuizResults(int QuizId);
        QuizResult AddQuizResult(QuizResult Quiz);
        QuizResult UpdateQuizResult(QuizResult Quiz);
        QuizResult DeleteQuizResult(int id);
    }
}
