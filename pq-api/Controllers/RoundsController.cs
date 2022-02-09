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
    public class RoundsController : ControllerBase
    {
        private IAppService appService;
        private IHttpContextAccessor httpContextAccessor;

        public RoundsController(IAppService appService, IHttpContextAccessor httpContextAccessor)
        {
            this.appService = appService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpGet("rounds")]
        public IEnumerable<M.Round> GetQuizzes()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<M.Round> rtn = appService.GetRounds(userId).Select(q => new M.Round
            {
                id = q.Id,
                quizId = q.QuizId,
                name = q.Name
            });

            return rtn;

        }

        [Authorize]
        [HttpGet("rounds/{RoundId}")]
        public M.Round GetRound(int RoundId)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var round = appService.GetRound(userId, RoundId);
            return new M.Round
            {
                id = round.Id,
                quizId = round.QuizId,
                name = round.Name

            };

        }

        [Authorize]
        [HttpPost("rounds/add")]
        public M.Round CreateRound(M.RoundCreate r)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var addedRound = appService.AddRound(userId, new B.Round { Name = r.Name, QuizId = r.QuizId });
            return new M.Round
            {
                id = addedRound.Id,
                name = addedRound.Name

            };

        }

        [Authorize]
        [HttpPost("rounds/update")]
        public M.Round UpdateQuiz(M.RoundUpdate r)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var updatedRound = appService.EditRound(userId, new B.Round { Id = r.Id, Name = r.Name, QuizId = r.QuizId });
            return new M.Round
            {
                id = updatedRound.Id,
                name = updatedRound.Name

            };

        }

        [Authorize]
        [HttpGet("rounds/{roundId}/questions")]
        public IEnumerable<M.Question> GetQuestionForRound(int roundId)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<M.Question> rtn = appService.GetQuestionsForRound(userId, roundId).Select(q => new M.Question
            {
                id = q.Id,
                roundId = q.RoundId,
                question = q.Question1,
                answer = q.Answer,
                Categories = q.Categories.Select(c => new M.Category { Id = c.Id, Name = c.Name }).ToList(),
                QuestionDifficulty = q.QuestionDifficulty
            });

            return rtn;
        }


    }
}
