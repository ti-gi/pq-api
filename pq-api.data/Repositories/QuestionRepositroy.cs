using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Microsoft.EntityFrameworkCore;
using pq_api.data.Entities;

namespace pq_api.data.Repositories
{
    public class QuestionRepositroy : IQuestionRepository
    {
        protected readonly pqsightcom_dev_core_1Context pqEntities;

        public QuestionRepositroy(pqsightcom_dev_core_1Context Entities)
        {
            pqEntities = Entities;
        }

        public Question Add(Question entity)
        {
            pqEntities.Questions.Add(entity);
            pqEntities.SaveChanges();
            return entity;
        }

        public Question Update(Question entity)
        {
            var existingQuestion = pqEntities.Questions.Where(q => q.QuestionIdPk == entity.QuestionIdPk).FirstOrDefault();
            if (existingQuestion != null)
            {
                existingQuestion.Question1 = entity.Question1;
                existingQuestion.Answer = entity.Answer;
                existingQuestion.QuestionDifficulty = entity.QuestionDifficulty;
            }
            pqEntities.SaveChanges();
            return existingQuestion;
        }

        public IEnumerable<Question> All()
        {
            //int tenantId = pqEntities.AspNetUsers.Where(u => u.UserName == System.Web.HttpContext.Current.User.Identity.Name).Select(u => u.TenantId).FirstOrDefault() ?? 0;
            //this.pqEntities = DataModel.DataModel.SelectDatabase(tenantId);
            return pqEntities.Questions.ToList();
        }

        public IEnumerable<Question> Find(Expression<Func<Question, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Question Get(int id)
        {
            return pqEntities.Questions.Where(q => q.QuestionIdPk == id).First();
        }

        public IEnumerable<QuestionCategory> GetCategoriesForQuestion(int QuestionId)
        {
            return pqEntities.QuestionCategories.Include(q => q.CategoryIdFkNavigation).Where(q => q.QuestionIdFk == QuestionId).ToList();
        }

        public QuestionCategory Add(QuestionCategory entity)
        {
            pqEntities.QuestionCategories.Add(entity);
            pqEntities.SaveChanges();
            return entity;
        }

        public QuestionCategory Delete(int QuestionCategoryId)
        {
            var questionCategory = pqEntities.QuestionCategories.Where(r => r.QuestionCategoryIdPk == QuestionCategoryId).First();
            pqEntities.QuestionCategories.Remove(questionCategory);
            pqEntities.SaveChanges();
            return questionCategory;
        }

        public Category Add(Category entity)
        {
            pqEntities.Categories.Add(entity);
            pqEntities.SaveChanges();
            return entity;
        }



        public IEnumerable<Category> GetCategories()
        {
            return pqEntities.Categories.ToList();
        }

        public IEnumerable<QuestionCategory> GetQuestionCategories(int id)
        {
            return pqEntities.QuestionCategories.Where(qc => qc.QuestionIdFk == id).ToList();
        }

        public IEnumerable<Question> GetQuestionsForRound(int RoundId)
        {
            return pqEntities.Questions.Include(q => q.QuestionCategories).ThenInclude(c => c.CategoryIdFkNavigation).
                Where(q => q.RoundIdFk == RoundId).ToList();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        
    }
}