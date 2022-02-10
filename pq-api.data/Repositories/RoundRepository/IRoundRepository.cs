using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using pq_api.data.Entities;

namespace pq_api.data.Repositories
{
    public interface IRoundRepository
    {
        IEnumerable<Round> All(string userId);
        IEnumerable<Round> GetRoundsForQuiz(string userId, int QuizId);
        Round Get(string userId, int id);
        Round Add(Round entity);
        Round Update(Round entity);
        
        int CountOfRoundsPerQuiz(string userId, int QuizId);
    }
}
