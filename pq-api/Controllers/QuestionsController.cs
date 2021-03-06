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
    public class QuestionsController : ControllerBase
    {
        private IAppService appService;
        private IHttpContextAccessor httpContextAccessor;

        public QuestionsController(IAppService appService, IHttpContextAccessor httpContextAccessor)
        {
            this.appService = appService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpGet("questions")]
        public IEnumerable<M.Question> GetQuestions()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<M.Question> rtn = appService.GetQuestions(userId).Select(q => new M.Question
            {
                id = q.Id,
                roundId = q.RoundId,
                question = q.Question1,
                answer = q.Answer,
                categories = q.Categories.Select(c => new M.Category { Id = c.Id, Name = c.Name }).ToList(),
                questionDifficulty = q.QuestionDifficulty,
                ord = q.Ord
            });

            return rtn;
        }

        [Authorize]
        [HttpGet("questions/{questionId}")]
        public M.Question GetQuestion(int questionId)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var question = appService.GetQuestion(userId, questionId);
            var questionCategories = question.Categories.Select(c => new M.Category
            {
                Id = c.Id,
                Name = c.Name

            }).ToList();

            return new M.Question
            {
                id = question.Id,
                roundId = question.RoundId,
                question = question.Question1,
                answer = question.Answer,
                categories = questionCategories,
                questionDifficulty = question.QuestionDifficulty,
                ord = question.Ord
            };

        }

        [Authorize]
        [HttpGet("questions/count")]
        public int GetQuestionCount()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int rtn = appService.GetQuestionCount(userId);
            return rtn;
        }

        [Authorize]
        [HttpPost("questions/add")]
        public M.Question AddQuestion(M.QuestionCreate q)
        {
            var categories = new List<B.Category>();
            foreach (var item in q.Categories)
            {
                categories.Add(new B.Category { Id = item.Id, Name = item.Name });
            }

            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var addedQuestion = appService.AddQuestion(userId, new B.Question { Question1 = q.Question, Answer = q.Answer, RoundId = q.RoundId, Categories = categories, QuestionDifficulty = q.QuestionDifficulty });
            return new M.Question
            {
                id = addedQuestion.Id,
                question = addedQuestion.Question1,
                answer = addedQuestion.Answer,
                categories = addedQuestion.Categories.Select(c => new M.Category { Id = c.Id, Name = c.Name }).ToList(),
                roundId = addedQuestion.RoundId,
                questionDifficulty = addedQuestion.QuestionDifficulty,
                ord = addedQuestion.Ord
            };
        }

        [Authorize]
        [HttpPost("questions/update")]
        public M.Question UpdateQuestion(M.QuestionUpdate q)
        {
            var categories = new List<B.Category>();
            foreach (var item in q.Categories)
            {
                categories.Add(new B.Category { Id = item.Id, Name = item.Name });
            }

            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var updatedQuestion = appService.UpdateQuestion(userId, new B.Question
            {
                Id = q.Id,
                Question1 = q.Question,
                Answer = q.Answer,
                RoundId = q.RoundId,
                Categories = categories,
                QuestionDifficulty = q.QuestionDifficulty
            });

            return new M.Question
            {
                id = updatedQuestion.Id,
                question = updatedQuestion.Question1,
                answer = updatedQuestion.Answer,
                categories = updatedQuestion.Categories.Select(c => new M.Category { Id = c.Id, Name = c.Name }).ToList(),
                roundId = updatedQuestion.RoundId,
                questionDifficulty = updatedQuestion.QuestionDifficulty,
                ord = updatedQuestion.Ord
            };
        }

        [Authorize]
        [HttpPost("questions/update-ord")]
        public M.Question UpdateQuestionOrd(M.QuestionUpdateOrd q)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var updatedQuestion = appService.UpdateQuestionOrd(userId, q.Id, q.Ord);

            return new M.Question
            {
                id = updatedQuestion.Id,
                question = updatedQuestion.Question1,
                answer = updatedQuestion.Answer,
                categories = updatedQuestion.Categories.Select(c => new M.Category { Id = c.Id, Name = c.Name }).ToList(),
                roundId = updatedQuestion.RoundId,
                questionDifficulty = updatedQuestion.QuestionDifficulty,
                ord = updatedQuestion.Ord
            };
        }

        [Authorize]
        [HttpDelete("questions/delete/{id}")]
        public M.Question DeleteQuestion(int id)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var deletedQuestion = appService.DeleteQuestion(userId, id);
            return new M.Question
            {
                id = deletedQuestion.Id,
                question = deletedQuestion.Question1,
                answer = deletedQuestion.Answer,
                categories = deletedQuestion.Categories.Select(c => new M.Category { Id = c.Id, Name = c.Name }).ToList(),
                roundId = deletedQuestion.RoundId,
                questionDifficulty = deletedQuestion.QuestionDifficulty,
                ord = deletedQuestion.Ord
            };
        }

        [Authorize]
        [HttpGet("questions/categories")]
        public IEnumerable<M.Category> GetCategories()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var categories = appService.GetCategories(userId).Select(c => new M.Category
            {
                Id = c.Id,
                Name = c.Name
            });

            return categories;

        }


    }
}
