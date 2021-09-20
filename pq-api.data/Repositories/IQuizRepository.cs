using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using pq_api.data.Entities;

namespace pq_api.data.Repositories
{
    public interface IQuizRepository : IRepository<Quiz>, IQuizResultRepository
    {
        IEnumerable<Quiz> GetQuizzesForCompetition(int CompetitionId);
        //IEnumerable<Round> GetRoundsForQuiz(int QuizId);
        IEnumerable<QuizResultFinal> QuizResultsFinal(int QuizId);
    }
}
