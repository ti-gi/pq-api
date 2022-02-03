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
    public class ContestantsController : ControllerBase
    {
        //private readonly pqsightcom_dev_core_1Context _context;

        private IAppService appService;

        public ContestantsController(IAppService appService)
        {
            this.appService = appService;
        }

        [Authorize]
        [HttpGet("contestants/{ContestantId}")]
        public M.Contestant GetContestant(int ContestantId)
        {

            var contestant = appService.GetContestant(ContestantId);
            return new M.Contestant
            {
                id = contestant.Id,
                competitionId = contestant.CompetitionId,
                name = contestant.Name

            };
        }

        [Authorize]
        [HttpPost("contestants/add")]
        public Response<M.Contestant> CreateContestant(M.ContestantCreate c)
        {

            var addedContestant = appService.AddContestant(new B.Contestant { Name = c.Name, CompetitionId = c.CompetitionId });
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

            var updatedContestant = appService.EditContestant(new B.Contestant { Id = c.Id, Name = c.Name, CompetitionId = c.CompetitionId });

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


        }
    }
