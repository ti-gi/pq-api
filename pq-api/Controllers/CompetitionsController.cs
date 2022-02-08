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
    public class CompetitionsController : ControllerBase
    {
        private IAppService appService;
        private IHttpContextAccessor httpContextAccessor;

        public CompetitionsController(IAppService appService, IHttpContextAccessor httpContextAccessor)
        {
            this.appService = appService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpGet("competitions")]
        public IEnumerable<M.Competition> GetCompetitions()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var results = appService.GetAllCompetitions(userId);
            var rtn = new List<M.Competition>();

            foreach (var r in results)
            {
                rtn.Add(new M.Competition { id = r.Id, name = r.Name, ord = 1 });
            }

            return rtn;
        }

        [Authorize]
        [HttpGet("competitions/{id}")]
        public M.Competition GetCompetition(int id)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var competition = appService.GetCompetition(userId, id);
            return new M.Competition
            {
                id = competition.Id,
                name = competition.Name

            };
        }

        [Authorize]
        [HttpPost("competitions/add")]
        public M.Competition AddCompetition(M.CompetitionCreate c)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var addedCompetition = appService.AddCompetition(userId, new B.Competition { Name = c.Name });
            return new M.Competition
            {
                id = addedCompetition.Id,
                name = addedCompetition.Name,
                ord = 1
            };

        }

        [Authorize]
        [HttpPost("competitions/update")]
        public M.Competition UpdateCompetition(M.CompetitionUpdate c)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var updatedCompetition = appService.UpdateCompetition(userId, new B.Competition { Id = c.Id, Name = c.Name });
            return new M.Competition
            {
                id = updatedCompetition.Id,
                name = updatedCompetition.Name

            };
        }

        [Authorize]
        [HttpGet("competitions/{competitionId}/contestants")]
        public object GetContestants(int competitionId)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<M.Contestant> rtn = appService.GetContestantsForCompetition(userId, competitionId).Select(q => new M.Contestant
            {
                id = q.Id,
                competitionId = q.CompetitionId,
                name = q.Name
            });

            return rtn;
        }

        [Authorize]
        [HttpGet("competitions/{id}/competition-results")]
        public  IEnumerable<M.CompetitionResult> GetCompetitionResults(int id)
        {
            var results = appService.CompetitionResults(id);
            var rtn = new List<M.CompetitionResult>();

            foreach (var r in results)
            {
                rtn.Add(new M.CompetitionResult
                {
                    Contestant = r.Contestant,
                    Points = r.Points
                });
            }

            return rtn;
        }

        
        [Authorize]
        [HttpGet("competitions/{competitionId}/quizzes")]
        public object GetQuizzes(int competitionId)
        {
            IEnumerable<M.Quiz> rtn = appService.GetQuizzesForCompetition(competitionId).Select(q => new M.Quiz
            {
                id = q.Id,
                competitionId = q.CompetitionId,
                name = q.Name
            });

            return rtn;
        }

    }
}
