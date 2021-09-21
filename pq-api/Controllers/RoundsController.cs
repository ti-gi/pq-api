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
    public class RoundsController : ControllerBase
    {
        //private readonly pqsightcom_dev_core_1Context _context;

        private IAppService appService;

        public RoundsController(IAppService appService)
        {
            this.appService = appService;
        }

        [Authorize]
        [HttpGet("rounds/{RoundId}")]
        public M.Round GetRound(int RoundId)
        {

            var round = appService.GetRound(RoundId);
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

            var addedRound = appService.AddRound(new B.Round { Name = r.Name, QuizId = r.QuizId });
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

            var updatedRound = appService.EditRound(new B.Round { Id = r.Id, Name = r.Name, QuizId = r.QuizId });
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
            IEnumerable<M.Question> rtn = appService.GetQuestionsForRound(roundId).Select(q => new M.Question
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
