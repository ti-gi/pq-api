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

        public IEnumerable<Question> GetQuestions(string userId)
        {
            return pqEntities.Questions.Where(q => q.UserId == userId).ToList();
        }

        public IEnumerable<Question> GetQuestionsForRound(string userId, int roundId)
        {
            return pqEntities.Questions.Include(q => q.QuestionCategories).ThenInclude(c => c.CategoryIdFkNavigation).
                Where(q => q.UserId == userId && q.RoundIdFk == roundId).ToList();
        }

        public Question Get(string userId, int id)
        {
            return pqEntities.Questions.Where(q => q.UserId == userId && q.QuestionIdPk == id).First();
        }

        public Question Add(Question entity)
        {
            pqEntities.Questions.Add(entity);
            pqEntities.SaveChanges();
            return entity;
        }

        public Question Update(Question entity)
        {
            var existingQuestion = pqEntities.Questions.Where(q => q.UserId == entity.UserId && q.QuestionIdPk == entity.QuestionIdPk).FirstOrDefault();
            if (existingQuestion != null)
            {
                existingQuestion.Question1 = entity.Question1;
                existingQuestion.Answer = entity.Answer;
                existingQuestion.QuestionDifficulty = entity.QuestionDifficulty;
            }
            pqEntities.SaveChanges();
            return existingQuestion;
        }

       
        public IEnumerable<QuestionCategory> GetCategoriesForQuestion(string userId, int QuestionId)
        {
            return pqEntities.QuestionCategories.Include(q => q.CategoryIdFkNavigation).Where(q => q.UserId == userId && q.QuestionIdFk == QuestionId).ToList();
        }

        public QuestionCategory Add(QuestionCategory entity)
        {
            pqEntities.QuestionCategories.Add(entity);
            pqEntities.SaveChanges();
            return entity;
        }

        public QuestionCategory Delete(string userId, int QuestionCategoryId)
        {
            var questionCategory = pqEntities.QuestionCategories.Where(qc => qc.UserId == userId && qc.QuestionCategoryIdPk == QuestionCategoryId).First();
            pqEntities.QuestionCategories.Remove(questionCategory);
            pqEntities.SaveChanges();
            return questionCategory;
        }

        public IEnumerable<QuestionCategory> GetQuestionCategories(string userId, int id)
        {
            return pqEntities.QuestionCategories.Where(qc => qc.UserId == userId && qc.QuestionIdFk == id).ToList();
        }


        #region Category
        public IEnumerable<Category> GetCategories(string userId)
        {
            return pqEntities.Categories.Where(c => c.UserId == userId).ToList();
        }

        public Category Add(Category entity)
        {
            pqEntities.Categories.Add(entity);
            pqEntities.SaveChanges();
            return entity;
        }

        #endregion

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        
    }
}