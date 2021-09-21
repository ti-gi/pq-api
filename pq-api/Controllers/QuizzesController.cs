using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pq_api.data.Entities;
using M = pq_api.Models;
using B = pq_api.service.BusinessModels;
using pq_api.service;

namespace pq_api.Controllers
{
    [Route("api")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        //private readonly pqsightcom_dev_core_1Context _context;

        private IAppService appService;

        public QuizzesController(IAppService appService)
        {
            this.appService = appService;
        }

        [Authorize]
        [HttpGet("quizzes/{QuizId}")]
        public M.Quiz GetQuiz(int QuizId)
        {

            var quiz = appService.GetQuiz(QuizId);
            return new M.Quiz
            {
                id = quiz.Id,
                competitionId = quiz.CompetitionId,
                name = quiz.Name

            };

        }

        [Authorize]
        [HttpPost("quizzes/add")]
        public M.Quiz CreateQuiz(M.QuizCreate c)
        {

            var addedQuiz = appService.AddQuiz(new B.Quiz { Name = c.Name, CompetitionId = c.CompetitionId });
            return new M.Quiz
            {
                id = addedQuiz.Id,
                name = addedQuiz.Name

            };

        }

        [Authorize]
        [HttpPost("quizzes/update")]
        public M.Quiz UpdateQuiz(M.QuizUpdate c)
        {

            var updatedQuiz = appService.EditQuiz(new B.Quiz { Id = c.Id, Name = c.Name, CompetitionId = c.CompetitionId });
            return new M.Quiz
            {
                id = updatedQuiz.Id,
                name = updatedQuiz.Name

            };

        }

        [Authorize]
        [HttpGet("quizzes/{QuizId}/rounds")]
        public IEnumerable<M.Round> GetRounds(int QuizId)
        {
            var results = appService.GetRoundsForQuiz(QuizId).Select(r => new M.Round
            {
                id = r.Id,
                quizId = r.QuizId,
                name = r.Name

            });

            return results;
        }

        [Authorize]
        [HttpGet("quizzes/{QuizId}/quiz-results-final")]
        public IEnumerable<M.QuizResultFinal> CompetitionResults(int QuizId)
        {
            var results = appService.QuizResultsFinal(QuizId);
            var rtn = new List<M.QuizResultFinal>();

            foreach (var r in results)
            {
                rtn.Add(new M.QuizResultFinal
                {
                    Contestant = r.Contestant,
                    Points = r.Points
                });
            }

            return rtn;
        }

        [Authorize]
        [HttpGet("quizzes/{QuizId}/quizresults")]
        public IEnumerable<M.QuizResult> GetQuizResults(int QuizId)
        {
            var results = appService.GetQuizResults(QuizId);
            var rtn = new List<M.QuizResult>();

            foreach (var r in results)
            {
                rtn.Add(new M.QuizResult
                {
                    Id = r.Id,
                    QuizId = r.QuizId,
                    ContestantId = r.ContestantId,
                    Contestant = r.Contestant,

                    Round1Points = r.Round1Points,
                    PointsAfterRound1 = r.PointsAfterRound1,
                    RoundResult1Id = r.RoundResult1Id,

                    Round2Points = r.Round2Points,
                    PointsAfterRound2 = r.PointsAfterRound2,
                    RoundResult2Id = r.RoundResult2Id,

                    Round3Points = r.Round3Points,
                    PointsAfterRound3 = r.PointsAfterRound3,
                    RoundResult3Id = r.RoundResult3Id,

                    Round4Points = r.Round4Points,
                    PointsAfterRound4 = r.PointsAfterRound4,
                    RoundResult4Id = r.RoundResult4Id
                });
            }

            return rtn;
        }

        [Authorize]
        [HttpPost("quizzes/{QuizId}/add-quiz-results")]
        public IEnumerable<M.QuizResult> AddQuizResults(IEnumerable<M.QuizResult> quizResults)
        {
            var res = quizResults.Select(r => new B.QuizResult
            {
                Id = r.Id,
                QuizId = r.QuizId,
                ContestantId = r.ContestantId,
                Contestant = r.Contestant,

                Round1Points = r.Round1Points,
                PointsAfterRound1 = r.PointsAfterRound1,
                RoundResult1Id = r.RoundResult1Id,

                Round2Points = r.Round2Points,
                PointsAfterRound2 = r.PointsAfterRound2,
                RoundResult2Id = r.RoundResult2Id,

                Round3Points = r.Round3Points,
                PointsAfterRound3 = r.PointsAfterRound3,
                RoundResult3Id = r.RoundResult3Id,

                Round4Points = r.Round4Points,
                PointsAfterRound4 = r.PointsAfterRound4,
                RoundResult4Id = r.RoundResult4Id
            }).ToList();

            var results = appService.AddQuizResults(res);
            var rtn = new List<M.QuizResult>();

            foreach (var r in results)
            {
                rtn.Add(new M.QuizResult
                {
                    Id = r.Id,
                    QuizId = r.QuizId,
                    ContestantId = r.ContestantId,

                    Round1Points = r.Round1Points,
                    PointsAfterRound1 = r.PointsAfterRound1,
                    RoundResult1Id = r.RoundResult1Id,

                    Round2Points = r.Round2Points,
                    PointsAfterRound2 = r.PointsAfterRound2,
                    RoundResult2Id = r.RoundResult2Id,

                    Round3Points = r.Round3Points,
                    PointsAfterRound3 = r.PointsAfterRound3,
                    RoundResult3Id = r.RoundResult3Id,

                    Round4Points = r.Round4Points,
                    PointsAfterRound4 = r.PointsAfterRound4,
                    RoundResult4Id = r.RoundResult4Id
                });
            }

            return rtn;
        }

        [Authorize]
        [HttpPost("quizzes/{QuizId}/update-quiz-results")]
        public IEnumerable<M.QuizResult> UpdateQuizResults(IEnumerable<M.QuizResult> quizResults)
        {
            var res = quizResults.Select(r => new B.QuizResult
            {
                Id = r.Id,
                QuizId = r.QuizId,
                ContestantId = r.ContestantId,

                Round1Points = r.Round1Points,
                PointsAfterRound1 = r.PointsAfterRound1,
                RoundResult1Id = r.RoundResult1Id,

                Round2Points = r.Round2Points,
                PointsAfterRound2 = r.PointsAfterRound2,
                RoundResult2Id = r.RoundResult2Id,

                Round3Points = r.Round3Points,
                PointsAfterRound3 = r.PointsAfterRound3,
                RoundResult3Id = r.RoundResult3Id,

                Round4Points = r.Round4Points,
                PointsAfterRound4 = r.PointsAfterRound4,
                RoundResult4Id = r.RoundResult4Id
            }).ToList();

            var results = appService.UpdateQuizResults(res);
            var rtn = new List<M.QuizResult>();

            foreach (var r in results)
            {
                rtn.Add(new M.QuizResult
                {
                    Id = r.Id,
                    QuizId = r.QuizId,
                    ContestantId = r.ContestantId,

                    Round1Points = r.Round1Points,
                    PointsAfterRound1 = r.PointsAfterRound1,
                    RoundResult1Id = r.RoundResult1Id,

                    Round2Points = r.Round2Points,
                    PointsAfterRound2 = r.PointsAfterRound2,
                    RoundResult2Id = r.RoundResult2Id,

                    Round3Points = r.Round3Points,
                    PointsAfterRound3 = r.PointsAfterRound3,
                    RoundResult3Id = r.RoundResult3Id,

                    Round4Points = r.Round4Points,
                    PointsAfterRound4 = r.PointsAfterRound4,
                    RoundResult4Id = r.RoundResult4Id
                });
            }

            return rtn;
        }


    }
}
