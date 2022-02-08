using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using pq_api.data.Entities;

namespace pq_api.data.Repositories
{
    public interface IQuizRepository : IQuizResultRepository
    {
        IEnumerable<Quiz> GetQuizzes(string userId);
        IEnumerable<Quiz> GetQuizzesForCompetition(int CompetitionId);
        Quiz Get(int id);
        Quiz Add(Quiz entity);
        Quiz Update(Quiz entity);
        
        IEnumerable<Round> GetRoundsForQuiz(int QuizId);
        IEnumerable<QuizResultFinal> QuizResultsFinal(int QuizId);
    }
}
