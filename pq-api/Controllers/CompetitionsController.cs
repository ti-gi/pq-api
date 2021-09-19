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

namespace pqWebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class CompetitionsController : ControllerBase
    {
        //private readonly pqsightcom_dev_core_1Context _context;

        private IAppService appService;

        public CompetitionsController(IAppService appService)
        {
            this.appService = appService;
        }

        // GET: api/Competitions
        [HttpGet("competitions")]
        [Authorize]
        public IEnumerable<M.Competition> GetCompetitions()
        {
            var results = appService.GetAllCompetitions();
            var rtn = new List<M.Competition>();

            foreach (var r in results)
            {
                rtn.Add(new M.Competition { id = r.Id, name = r.Name, ord = 1 });
            }

            return rtn;
            //var results = _context.Competitions.ToList();
            //var rtn = new List<M.Competition>();

            //foreach (var r in results)
            //{
            //    rtn.Add(new M.Competition { id = r.CompetitionIdPk, name = r.Name, ord = 1 });
            //}

            //return rtn;

            
        }

        // GET: api/Competitions/5
        [HttpGet("competitions/{id}")]
        public M.Competition GetCompetition(int id)
        {
            var competition = appService.GetCompetition(id);
            return new M.Competition
            {
                id = competition.Id,
                name = competition.Name

            };
            //return new M.Competition { id = 5, name = "aaaa", ord = 1 } ;
        }

        // GET: api/Competitions/5
        [HttpGet("competitions/{id}/competition-results")]
        public  IEnumerable<M.CompetitionResult> GetCompetitionResults(int id)
        {
            //var results = _context.CompetitionResults.FromSqlRaw("Get_CompetitonResults @p0", "1").ToList();

            var rtn = new List<M.CompetitionResult>();
            rtn.Add(new M.CompetitionResult { Contestant = "aaa", Points = 5 });
            rtn.Add(new M.CompetitionResult { Contestant = "bbbb", Points = 6 });
            //foreach (var r in results)
            //{
            //    rtn.Add(
            //}

            return rtn;

        }

        // PUT: api/Competitions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("competitions/{id}")]
        //public async Task<IActionResult> PutCompetition(int id, Competition competition)
        //{
        //    if (id != competition.CompetitionIdPk)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(competition).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CompetitionExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Competitions
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Competition>> PostCompetition(Competition competition)
        //{
        //    _context.Competitions.Add(competition);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetCompetition", new { id = competition.CompetitionIdPk }, competition);
        //}
        [Authorize]
        [HttpPost("competitions/add")]
        public M.Competition CreateCompetition(M.CompetitionCreate c)
        {

            var addedCompetition = appService.AddCompetition(new B.Competition { Name = c.Name });
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
            var updatedCompetition = appService.EditCompetition(new B.Competition { Id = c.Id, Name = c.Name });
            return new M.Competition
            {
                id = updatedCompetition.Id,
                name = updatedCompetition.Name

            };
            //return new M.Competition { id = 5, name = "aaaa", ord = 1 };
            //var updatedCompetition = appService.EditCompetition(new B.Competition { Id = c.Id, Name = c.Name });
            //return new M.Competition
            //{
            //    id = updatedCompetition.Id,
            //    name = updatedCompetition.Name

            //};

        }

        [Authorize]
        [HttpGet("competitions/{competitionId}/contestants")]
        public object GetContestants(int competitionId)
        {
            IEnumerable<M.Contestant> rtn = appService.GetContestantsForCompetition(competitionId).Select(q => new M.Contestant
            {
                id = q.Id,
                competitionId = q.CompetitionId,
                name = q.Name
            });

            return rtn;
        }

        //// DELETE: api/Competitions/5
        //[HttpDelete("competitions{id}")]
        //public async Task<IActionResult> DeleteCompetition(int id)
        //{
        //    var competition = await _context.Competitions.FindAsync(id);
        //    if (competition == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Competitions.Remove(competition);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool CompetitionExists(int id)
        //{
        //    return _context.Competitions.Any(e => e.CompetitionIdPk == id);
        //}
    }
}
