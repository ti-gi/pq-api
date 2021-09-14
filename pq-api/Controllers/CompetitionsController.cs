using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pq_api.data.Models;

namespace pqWebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class CompetitionsController : ControllerBase
    {
        private readonly pqsightcom_dev_core_1Context _context;

        public CompetitionsController(pqsightcom_dev_core_1Context context)
        {
            _context = context;
        }

        // GET: api/Competitions
        [HttpGet("competitions")]
        public async Task<ActionResult<IEnumerable<Competition>>> GetCompetitions()
        {
            return await _context.Competitions.ToListAsync();
        }

        // GET: api/Competitions/5
        [HttpGet("competitions/{id}")]
        public async Task<ActionResult<Competition>> GetCompetition(int id)
        {
            var competition = await _context.Competitions.FindAsync(id);

            if (competition == null)
            {
                return NotFound();
            }

            return competition;
        }

        // GET: api/Competitions/5
        [HttpGet("competitions-results/{id}")]
        public  IEnumerable<CompetitionResults> GetCompetitionResults(int id)
        {
            var results = _context.CompetitionResults.FromSqlRaw("Get_CompetitonResults @p0", "1").ToList();

            var rtn = new List<CompetitionResults>();

            foreach (var r in results)
            {
                rtn.Add(new CompetitionResults { Name = r.Name, Points = r.Points});
            }

            return rtn;

        }

        // PUT: api/Competitions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("competitions/{id}")]
        public async Task<IActionResult> PutCompetition(int id, Competition competition)
        {
            if (id != competition.CompetitionIdPk)
            {
                return BadRequest();
            }

            _context.Entry(competition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetitionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Competitions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Competition>> PostCompetition(Competition competition)
        {
            _context.Competitions.Add(competition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompetition", new { id = competition.CompetitionIdPk }, competition);
        }

        // DELETE: api/Competitions/5
        [HttpDelete("competitions{id}")]
        public async Task<IActionResult> DeleteCompetition(int id)
        {
            var competition = await _context.Competitions.FindAsync(id);
            if (competition == null)
            {
                return NotFound();
            }

            _context.Competitions.Remove(competition);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompetitionExists(int id)
        {
            return _context.Competitions.Any(e => e.CompetitionIdPk == id);
        }
    }
}
