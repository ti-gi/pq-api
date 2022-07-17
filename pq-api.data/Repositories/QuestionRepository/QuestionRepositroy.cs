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

        #region Question
        public IEnumerable<Question> GetQuestions(string userId)
        {
            return pqEntities.Questions.Include(q => q.QuestionCategories)
                                    .ThenInclude(c => c.CategoryIdFkNavigation)
                                    .Where(q => q.UserId == userId)
                                    .OrderBy(q => q.RoundIdFk)
                                    .ThenBy(q => q.QuestionIdPk).ToList();
        }

        public IEnumerable<Question> GetQuestionsForRound(string userId, int roundId)
        {
            return pqEntities.Questions.Include(q => q.QuestionCategories)
                                    .ThenInclude(c => c.CategoryIdFkNavigation)
                                    .Where(q => q.UserId == userId && q.RoundIdFk == roundId)
                                    .OrderBy(q => q.RoundIdFk)
                                    .ThenBy(q => q.QuestionIdPk).ToList();
        }

        public int GetNextOrd(string userId, int roundId)
        {
            int lastOrd = pqEntities.Questions.Where(q => q.UserId == userId && q.RoundIdFk == roundId).Count();
            return lastOrd + 1;
        }

        public Question Get(string userId, int id)
        {
            return pqEntities.Questions
                            .Include(q => q.QuestionCategories)
                            .ThenInclude(c => c.CategoryIdFkNavigation)
                            .Where(q => q.UserId == userId && q.QuestionIdPk == id).FirstOrDefault();
        }

        public int GetQuestionCount(string userId)
        {
            return pqEntities.Questions.Where(q => q.UserId == userId).Count();
        }

        public Question Add(Question entity)
        {
            pqEntities.Questions.Add(entity);
            pqEntities.SaveChanges();
            var newQuestion = pqEntities.Questions
                                           .Include(q => q.QuestionCategories)
                                           .ThenInclude(c => c.CategoryIdFkNavigation)
                                           .Where(q => q.UserId == entity.UserId && q.QuestionIdPk == entity.QuestionIdPk).FirstOrDefault();
            return newQuestion;
        }

        public Question Update(Question entity)
        {
            var existingQuestion = pqEntities.Questions
                                            .Include(q => q.QuestionCategories)
                                            .ThenInclude(c => c.CategoryIdFkNavigation)
                                            .Where(q => q.UserId == entity.UserId && q.QuestionIdPk == entity.QuestionIdPk).FirstOrDefault();
            if (existingQuestion != null)
            {
                existingQuestion.Question1 = entity.Question1;
                existingQuestion.Answer = entity.Answer;
                existingQuestion.QuestionDifficulty = entity.QuestionDifficulty;
                existingQuestion.RoundIdFk = entity.RoundIdFk;
                existingQuestion.Ord = entity.Ord;
            }
            pqEntities.SaveChanges();
            return existingQuestion;
        }

        public Question UpdateOrd(string userId, int id, int? ord)
        {
            var existingQuestion = pqEntities.Questions
                                            .Include(q => q.QuestionCategories)
                                            .ThenInclude(c => c.CategoryIdFkNavigation)
                                            .Where(q => q.UserId == userId && q.QuestionIdPk == id).FirstOrDefault();
            if (existingQuestion != null)
            {
                existingQuestion.Ord = ord;
            }
            pqEntities.SaveChanges();
            return existingQuestion;
        }

        public Question Delete(string userId, int id)
        {
            var existingQuestion = pqEntities.Questions
                                    .Include(q => q.QuestionCategories)
                                    .ThenInclude(c => c.CategoryIdFkNavigation)
                                    .Where(q => q.UserId == userId && q.QuestionIdPk == id).FirstOrDefault();
            //delete categories first
            var questionCategories = pqEntities.QuestionCategories.Where(q => q.UserId == userId && q.QuestionIdFk == id);
            foreach (var item in questionCategories)
            {
                pqEntities.QuestionCategories.Remove(item);
            }
            pqEntities.Questions.Remove(existingQuestion);
            pqEntities.SaveChanges();
            return existingQuestion;
        }
        #endregion

        #region QuestionCategory
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

        public QuestionCategory DeleteQuestionCategory(string userId, int QuestionCategoryId)
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
        #endregion

        #region Category
        public IEnumerable<Category> GetCategories(string userId)
        {
            return pqEntities.Categories.Where(c => c.UserId == userId).ToList();
        }

        public IEnumerable<CompetitionCategoryCount> GetCategoriesForCompetitionCount(string userId, int competitionId)
        {
            return pqEntities.CompetitionCategoryCounts.FromSqlRaw("Get_CategoriesForCompetitionCount @p0, @p1", userId, competitionId).ToList();  
        }

        public Category Add(Category entity)
        {
            pqEntities.Categories.Add(entity);
            pqEntities.SaveChanges();
            return entity;
        }

        #endregion
   
    }
}