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
    public class ContestantsController : ControllerBase
    {

        private IAppService appService;
        private IHttpContextAccessor httpContextAccessor;

        public ContestantsController(IAppService appService, IHttpContextAccessor httpContextAccessor)
        {
            this.appService = appService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpGet("contestants/{contestantId}")]
        public M.Contestant GetContestant(int contestantId)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var contestant = appService.GetContestant(userId, contestantId);
            return new M.Contestant
            {
                id = contestant.Id,
                competitionId = contestant.CompetitionId,
                name = contestant.Name

            };
        }

        [Authorize]
        [HttpPost("contestants/add")]
        public Response<M.Contestant> AddContestant(M.ContestantCreate c)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var addedContestant = appService.AddContestant(userId, new B.Contestant { Name = c.Name, CompetitionId = c.CompetitionId });
            M.Contestant contestant = null;
            if (addedContestant.Data != null)
            {
                contestant = new M.Contestant
                {
                    id = addedContestant.Data.Id,
                    name = addedContestant.Data.Name,
                    competitionId = addedContestant.Data.CompetitionId

                };
            }

            return new Response<M.Contestant>
            {
                Data = contestant,
                Message = addedContestant.Message
            };

        }

        [Authorize]
        [HttpPost("contestants/update")]
        public Response<M.Contestant> UpdateContestant(M.ContestantUpdate c)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var updatedContestant = appService.UpdateContestant(userId, new B.Contestant { Id = c.Id, Name = c.Name, CompetitionId = c.CompetitionId });
            
            M.Contestant contestant = null;
            if (updatedContestant.Data != null)
            {
                contestant = new M.Contestant
                {
                    id = updatedContestant.Data.Id,
                    name = updatedContestant.Data.Name,
                    competitionId = updatedContestant.Data.CompetitionId

                };
            }

            return new Response<M.Contestant>
            {
                Data = contestant,
                Message = updatedContestant.Message
            };
        }

        [Authorize]
        [HttpDelete("contestants/delete/{id}")]
        public Response<M.Contestant> DeleteContestant(int id, [FromQuery(Name = "deleteConfirmed")] bool deleteConfirmed)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var deletedContestant = appService.DeleteContestant(userId, id, deleteConfirmed);
            M.Contestant contestant = null;

            if (deletedContestant.Data != null)
            {
                contestant = new M.Contestant
                {
                    id = deletedContestant.Data.Id,
                    name = deletedContestant.Data.Name,
                    competitionId = deletedContestant.Data.CompetitionId

                };
            }

            return new Response<M.Contestant>
            {
                Data = contestant,
                Message = deletedContestant.Message
            };
        }

        [Authorize]
        [HttpGet("competitions/{competitionId}/contestant-wins")]
        public IEnumerable<M.ContestantWins> GetContestantWins(int competitionId)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var contestantWins = appService.GetContestantWins(userId, competitionId).Select(q => new M.ContestantWins
            {
                contestant = q.Contestant,
                wins = q.Wins,
                competitionId = competitionId
            });
            return contestantWins;
        }

    }
}
