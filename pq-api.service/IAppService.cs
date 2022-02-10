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
        #region Competition
        IEnumerable<B.Competition> GetCompetitions(string userId);
        B.Competition GetCompetition(string userId, int CompetitionId);
        B.Competition AddCompetition(string userId, B.Competition competition);
        B.Competition UpdateCompetition(string userId, B.Competition competition);
        IEnumerable<B.CompetitionResult> GetCompetitionResults(int CompetitionId);
        #endregion

        #region Contestant
        IEnumerable<B.Contestant> GetContestantsForCompetition(string userId, int CompetitionId);
        B.Contestant GetContestant(string userId, int ContestantId);
        Response<B.Contestant> AddContestant(string userId, B.Contestant contestant);
        Response<B.Contestant> UpdateContestant(string userId, B.Contestant contestant);
        Response<B.Contestant> DeleteContestant(string userId, int id, bool deleteConfirmed);
        #endregion

        #region Quiz
        IEnumerable<B.Quiz> GetQuizzes(string userId);
        IEnumerable<B.Quiz> GetQuizzesForCompetition(string userId, int CompetitionId);
        B.Quiz GetQuiz(string userId, int QuizId);
        B.Quiz AddQuiz(string userId, B.Quiz quiz);
        B.Quiz UpdateQuiz(string userId, B.Quiz quiz);
        IEnumerable<B.QuizResult> GetQuizResults(string userId, int QuizId);
        IEnumerable<B.QuizResult> AddQuizResults(string userId, IEnumerable<B.QuizResult> QuizResults);
        IEnumerable<B.QuizResult> UpdateQuizResults(string userId, IEnumerable<B.QuizResult> QuizResults);
        B.QuizResult DeleteQuizResult(string userId, int id);
        IEnumerable<B.QuizResultFinal> GetQuizResultsFinal(string userId, int QuizId);
        #endregion

        #region Round
        IEnumerable<B.Round> GetRounds(string userId);
        IEnumerable<B.Round> GetRoundsForQuiz(string userId, int quizId);
        B.Round GetRound(string userId, int roundId);
        B.Round AddRound(string userId, B.Round entity);
        B.Round UpdateRound(string userId, B.Round entity);
        #endregion

        #region Questions
        IEnumerable<B.Question> GetQuestionsForRound(string userId, int roundId);
        B.Question GetQuestion(string userId, int questionId);
        B.Question AddQuestion(string userId, B.Question question);
        B.Question UpdateQuestion(string userId, B.Question question);
        IEnumerable<B.Category> GetCategories(string userId);
        #endregion
    }
}
