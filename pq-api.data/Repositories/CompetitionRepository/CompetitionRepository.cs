﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Microsoft.EntityFrameworkCore;
using pq_api.data.Entities;

//using pq_web_api.DataModel;

namespace pq_api.data.Repositories
{
    public class CompetitionRepository : ICompetitionRepository
    {
        protected readonly pqsightcom_dev_core_1Context pqEntities;

        public CompetitionRepository(pqsightcom_dev_core_1Context Entities)
        {
            pqEntities = Entities;

            //pqEntities = new pqEntities("pqEntities2");
        }

        public IEnumerable<Competition> GetCompetitions(string userId)
        {
            return pqEntities.Competitions.Where(c => c.UserId == userId).ToList();
        }

        public Competition Get(int id)
        {
            return pqEntities.Competitions.Where(c => c.CompetitionIdPk == id).First();
        }

        public Competition Add(Competition entity)
        {
            //int tenantId = pqEntities.AspNetUsers.Where(u => u.UserName == System.Web.HttpContext.Current.User.Identity.Name).Select(u => u.TenantId).FirstOrDefault() ?? 0;
            //entity.Tenant_ID_FK = tenantId;
            pqEntities.Competitions.Add(entity);
            pqEntities.SaveChanges();
            return entity;
        }

        public Competition Update(Competition entity)
        {
            var existingCompetition = pqEntities.Competitions.Where(q => q.CompetitionIdPk == entity.CompetitionIdPk).FirstOrDefault();
            if (existingCompetition != null)
            {
                existingCompetition.Name = entity.Name;
            }
            pqEntities.SaveChanges();
            return existingCompetition;
        }

        public IEnumerable<CompetitionResults> CompetitionResults(int CompetitionId)
        {
            return pqEntities.CompetitionResults.FromSqlRaw("Get_CompetitonResults @p0", CompetitionId).ToList();

        }
    }
}