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
using System.Security.Claims;

namespace pq_api.Controllers
{
    [Route("api")]
    [ApiController]
    public class QuizzesController : ControllerBase
    {
        //private readonly pqsightcom_dev_core_1Context _context;

        private IAppService appService;
        private IHttpContextAccessor httpContextAccessor;

        public QuizzesController(IAppService appService, IHttpContextAccessor httpContextAccessor)
        {
            this.appService = appService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpGet("quizzes")]
        public IEnumerable<M.Quiz> GetQuizzes()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<M.Quiz> rtn = appService.GetQuizzes(userId).Select(q => new M.Quiz
            {
                id = q.Id,
                competitionId = q.CompetitionId,
                name = q.Name
            });

            return rtn;

        }

        [Authorize]
        [HttpGet("quizzes/{QuizId}")]
        public M.Quiz GetQuiz(int QuizId)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var quiz = appService.GetQuiz(userId, QuizId);
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
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var addedQuiz = appService.AddQuiz(userId, new B.Quiz { Name = c.Name, CompetitionId = c.CompetitionId });
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
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var updatedQuiz = appService.EditQuiz(userId, new B.Quiz { Id = c.Id, Name = c.Name, CompetitionId = c.CompetitionId });
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
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var results = appService.GetRoundsForQuiz(userId, QuizId).Select(r => new M.Round
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
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var results = appService.QuizResultsFinal(userId, QuizId);
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
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var results = appService.GetQuizResults(userId, QuizId);
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

            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var results = appService.AddQuizResults(userId, res);
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

            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var results = appService.UpdateQuizResults(userId, res);
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
        [HttpDelete("quizzes/{QuizId}/delete-quiz-result/{id}")]
        public M.QuizResult DeleteQuizResult(int id)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var r = appService.DeleteQuizResult(userId, id);

            return new M.QuizResult
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
            };
        }


    }
}
