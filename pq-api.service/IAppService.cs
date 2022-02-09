using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using B = pq_api.service.BusinessModels;

namespace pq_api.service
{
    public interface IAppService
    {
        IEnumerable<B.Competition> GetAllCompetitions(string userId);
        B.Competition GetCompetition(string userId, int CompetitionId);
        B.Competition AddCompetition(string userId, B.Competition competition);
        B.Competition UpdateCompetition(string userId, B.Competition competition);
        IEnumerable<B.Contestant> GetContestantsForCompetition(string userId, int CompetitionId);
        IEnumerable<B.CompetitionResult> CompetitionResults(int CompetitionId);

        B.Contestant GetContestant(string userId, int ContestantId);
        Response<B.Contestant> AddContestant(string userId, B.Contestant contestant);
        Response<B.Contestant> EditContestant(string userId, B.Contestant contestant);
        Response<B.Contestant> DeleteContestant(string userId, int id, bool deleteConfirmed);

        IEnumerable<B.Quiz> GetQuizzes(string userId);
        IEnumerable<B.Quiz> GetQuizzesForCompetition(string userId, int CompetitionId);
        B.Quiz GetQuiz(string userId, int QuizId);
        B.Quiz AddQuiz(string userId, B.Quiz quiz);
        B.Quiz EditQuiz(string userId, B.Quiz quiz);

        IEnumerable<B.QuizResult> GetQuizResults(string userId, int QuizId);
        //IEnumerable<B.QuizResult> RefreshQuizResults(int QuizId);
        IEnumerable<B.QuizResult> AddQuizResults(string userId, IEnumerable<B.QuizResult> QuizResults);
        IEnumerable<B.QuizResult> UpdateQuizResults(string userId, IEnumerable<B.QuizResult> QuizResults);
        B.QuizResult DeleteQuizResult(string userId, int id);
        IEnumerable<B.QuizResultFinal> QuizResultsFinal(string userId, int QuizId);

        IEnumerable<B.Round> GetRounds();
        B.Round GetRound(int RoundId);
        B.Round AddRound(B.Round entity);
        B.Round EditRound(B.Round entity);
        IEnumerable<B.Round> GetRoundsForQuiz(int QuizId);

        IEnumerable<B.Question> GetQuestionsForRound(string userId, int roundId);
        B.Question GetQuestion(string userId, int QuestionId);
        B.Question AddQuestion(string userId, B.Question question);
        B.Question EditQuestion(string userId, B.Question question);
        IEnumerable<B.Category> GetCategories(string userId);

        //IEnumerable<B.QuestionCategory> GetQuestionCategories(int id);
        
        
        //IEnumerable<B.Question> GetQuestions();
        

        //B.RoundResult GetRoundResult(int roundResultId);
        //B.RoundResult AddRoundResult(B.RoundResult roundResult);
        //B.RoundResult EditRoundResult(B.RoundResult roundResult);
        //B.RoundResult DeleteRoundResult(int roundResultId);
        //IEnumerable<B.RoundResult> GetRoundResultsForRound(int RoundId);



    }
}
