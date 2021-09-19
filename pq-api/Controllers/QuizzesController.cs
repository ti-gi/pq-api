﻿using System;
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


    }
}
