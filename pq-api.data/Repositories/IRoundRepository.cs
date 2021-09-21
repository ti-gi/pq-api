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
        Round Add(Round entity);
        Round Update(Round entity);
        Round Get(int id);

        //RoundResult GetRoundResult(int RoundResultId);
        //RoundResult AddRoundResult(RoundResult entity);
        //RoundResult UpdateRoundResult(RoundResult entity);
        //RoundResult DeleteRoundResult(int RoundResultId);
        //IEnumerable<RoundResult> GetRoundResultsForRound(int RoundId);

        int CountOfRoundsPerQuiz(int QuizId);
    }
}
