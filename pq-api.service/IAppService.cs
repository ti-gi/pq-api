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
        B.Competition GetCompetition(int CompetitionId);
        IEnumerable<B.Competition> GetAllCompetitions(string userId);
        B.Competition AddCompetition(B.Competition competition, string userId);
        B.Competition EditCompetition(B.Competition competition);
        IEnumerable<B.CompetitionResult> CompetitionResults(int CompetitionId);

        IEnumerable<B.Quiz> GetQuizzes();
        IEnumerable<B.Quiz> GetQuizzesForCompetition(int CompetitionId);
        IEnumerable<B.Contestant> GetContestantsForCompetition(int CompetitionId);
        B.Quiz GetQuiz(int QuizId);
        B.Quiz AddQuiz(B.Quiz quiz);
        B.Quiz EditQuiz(B.Quiz quiz);

        IEnumerable<B.QuizResult> GetQuizResults(int QuizId);
        //IEnumerable<B.QuizResult> RefreshQuizResults(int QuizId);
        IEnumerable<B.QuizResult> AddQuizResults(IEnumerable<B.QuizResult> QuizResults);
        IEnumerable<B.QuizResult> UpdateQuizResults(IEnumerable<B.QuizResult> QuizResults);
        B.QuizResult DeleteQuizResult(int id);
        IEnumerable<B.QuizResultFinal> QuizResultsFinal(int QuizId);

        IEnumerable<B.Round> GetRounds();
        B.Round GetRound(int RoundId);
        B.Round AddRound(B.Round entity);
        B.Round EditRound(B.Round entity);
        IEnumerable<B.Round> GetRoundsForQuiz(int QuizId);

        IEnumerable<B.Question> GetQuestionsForRound(int RoundId);
        IEnumerable<B.Category> GetCategories();
        //IEnumerable<B.QuestionCategory> GetQuestionCategories(int id);
        B.Question AddQuestion(B.Question question);
        B.Question GetQuestion(int QuestionId);
        //IEnumerable<B.Question> GetQuestions();
        B.Question EditQuestion(B.Question question);

        //B.RoundResult GetRoundResult(int roundResultId);
        //B.RoundResult AddRoundResult(B.RoundResult roundResult);
        //B.RoundResult EditRoundResult(B.RoundResult roundResult);
        //B.RoundResult DeleteRoundResult(int roundResultId);
        //IEnumerable<B.RoundResult> GetRoundResultsForRound(int RoundId);

        B.Contestant GetContestant(int ContestantId);
        Response<B.Contestant> AddContestant(B.Contestant contestant);
        Response<B.Contestant> EditContestant(B.Contestant contestant);

    }
}
