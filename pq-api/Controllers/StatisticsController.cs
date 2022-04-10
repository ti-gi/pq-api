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
    public class StatisticsController : ControllerBase
    {
        private IStatisticsService statisticsService;
        private IHttpContextAccessor httpContextAccessor;

        public StatisticsController(IStatisticsService statisticsService, IHttpContextAccessor httpContextAccessor)
        {
            this.statisticsService = statisticsService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpGet("statistics/categories")]
        public IEnumerable<M.CategoryCount> GetCategoriesCount(int competitionId)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            IEnumerable<M.CategoryCount> rtn = statisticsService.GetCategoriesCount(userId).Select(q => new M.CategoryCount
            {
                category = q.Category,
                count = q.Count
            });

            return rtn;
        }

    }
}
