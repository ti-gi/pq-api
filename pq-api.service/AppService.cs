using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using pq_api.data.Repositories;
using B = pq_api.service.BusinessModels;
using E = pq_api.data.Entities;


namespace pq_api.service
{
    public class AppService : IAppService
    {
        private ICompetitionRepository competitionRepository;
        private IContestantRepository contestantRepository;
        private IQuizRepository quizRepository;
        private IRoundRepository roundRepository;
        private IQuestionRepository questionRepository;

        public AppService(ICompetitionRepository competitionRepository, IContestantRepository contestantRepository, IQuizRepository quizRepository, IRoundRepository roundRepository, IQuestionRepository questionRepository)
        {
            this.competitionRepository = competitionRepository;
            this.contestantRepository = contestantRepository;
            this.quizRepository = quizRepository;
            this.roundRepository = roundRepository;
            this.questionRepository = questionRepository;
        }

        #region Competition

        public B.Competition GetCompetition(int CompetitionId)
        {
            var competition = competitionRepository.Get(CompetitionId);
            B.Competition rtn = new B.Competition { Id = competition.CompetitionIdPk, Name = competition.Name };
            return rtn;
        }

        public B.Competition AddCompetition(B.Competition competition)
        {
            var addedCompetition = competitionRepository.Add(new E.Competition { Name = competition.Name });
            B.Competition rtn = new B.Competition { Id = addedCompetition.CompetitionIdPk, Name = addedCompetition.Name };
            return rtn;
        }

        public B.Competition EditCompetition(B.Competition competition)
        {
            var updatedCompetition = competitionRepository.Update(new E.Competition { CompetitionIdPk = competition.Id, Name = competition.Name});
            B.Competition rtn = new B.Competition { Id = updatedCompetition.CompetitionIdPk, Name = updatedCompetition.Name };
            return rtn;
        }

        public IEnumerable<B.Competition> GetAllCompetitions()
        {
            IEnumerable<B.Competition> rtn = competitionRepository.All().Select( c => new B.Competition { Id = c.CompetitionIdPk, Name = c.Name });
            return rtn;
        }

        public IEnumerable<B.CompetitionResult> CompetitionResults(int CompetitionId)
        {
      
            IEnumerable<B.CompetitionResult> rtn = competitionRepository.CompetitionResults(CompetitionId).Select(cr => new B.CompetitionResult
            {
                Contestant = cr.Name,
                Points = cr.Points
            });

            return rtn;
        }

        #endregion

        #region Quiz

        public IEnumerable<B.Quiz> GetQuizzesForCompetition(int CompetitionId)
        {
            IEnumerable<B.Quiz> rtn = quizRepository.GetQuizzesForCompetition(CompetitionId).Select(q => new B.Quiz
            {
                Id = q.QuizIdPk,
                CompetitionId = q.CompetitionIdFk,
                Name = q.Name
            });

            return rtn;
        }

        public IEnumerable<B.QuizResultFinal> QuizResultsFinal(int QuizId)
        {

            IEnumerable<B.QuizResultFinal> rtn = quizRepository.QuizResultsFinal(QuizId).Select(cr => new B.QuizResultFinal
            {
                Contestant = cr.Name,
                Points = cr.Points
            });

            return rtn;
        }

        public B.Quiz GetQuiz(int QuizId)
        {
            var quiz = quizRepository.Get(QuizId);
            B.Quiz rtn = new B.Quiz { Id = quiz.QuizIdPk, Name = quiz.Name, CompetitionId = quiz.CompetitionIdFk };
            return rtn;
        }

        public B.Quiz AddQuiz(B.Quiz quiz)
        {
            var addedQuiz = quizRepository.Add(new E.Quiz { Name = quiz.Name, CompetitionIdFk = quiz.CompetitionId });
            B.Quiz rtn = new B.Quiz { Id = addedQuiz.QuizIdPk, Name = addedQuiz.Name, CompetitionId = addedQuiz.CompetitionIdFk };
            return rtn;
        }

        public B.Quiz EditQuiz(B.Quiz quiz)
        {
            var updatedQuiz = quizRepository.Update(new E.Quiz { QuizIdPk = quiz.Id, Name = quiz.Name, CompetitionIdFk = quiz.CompetitionId });
            B.Quiz rtn = new B.Quiz { Id = updatedQuiz.QuizIdPk, Name = updatedQuiz.Name, CompetitionId = updatedQuiz.CompetitionIdFk };
            return rtn;
        }
        #endregion Quiz

        //Rounds

        public B.Round GetRound(int RoundId)
        {
            var round = roundRepository.Get(RoundId);
            B.Round rtn = new B.Round { Id = round.RoundIdPk, Name = round.Name, QuizId = round.QuizIdFk };
            return rtn;
        }

        public B.Round AddRound(B.Round round)
        {
            var addedRound = roundRepository.Add(new E.Round { Name = round.Name, QuizIdFk = round.QuizId });
            B.Round rtn = new B.Round { Id = addedRound.RoundIdPk, Name = addedRound.Name, QuizId = addedRound.QuizIdFk };
            return rtn;
        }

        public B.Round EditRound(B.Round round)
        {
            var updatedRound = roundRepository.Update(new E.Round { RoundIdPk = round.Id, Name = round.Name, QuizIdFk = round.QuizId });
            B.Round rtn = new B.Round { Id = updatedRound.RoundIdPk, Name = updatedRound.Name, QuizId = updatedRound.QuizIdFk };
            return rtn;
        }

        public IEnumerable<B.Round> GetRoundsForQuiz(int QuizId)
        {
            IEnumerable<B.Round> rtn = quizRepository.GetRoundsForQuiz(QuizId).Select(q => new B.Round
            {
                Id = q.RoundIdPk,
                QuizId = q.QuizIdFk,
                Name = q.Name
            });

            return rtn;
        }

        public IEnumerable<B.Question> GetQuestionsForRound(int RoundId)
        {
            IEnumerable<B.Question> rtn = questionRepository.GetQuestionsForRound(RoundId).Select(q => new B.Question
            {
                Id = q.QuestionIdPk,
                RoundId = q.RoundIdFk,
                Question1 = q.Question1,
                Answer = q.Answer,
                Categories = q.QuestionCategories.Select(c => new B.Category { Id = c.CategoryIdFkNavigation.CategoryIdPk, Name = c.CategoryIdFkNavigation.Name }).ToList(),
                QuestionDifficulty = q.QuestionDifficulty

            });

            return rtn;
        }

        public IEnumerable<B.Category> GetCategories()
        {
            IEnumerable<B.Category> rtn = questionRepository.GetCategories().Select(c => new B.Category
            {
                Id = c.CategoryIdPk,
                Name = c.Name
            });

            return rtn;
        }

        //public IEnumerable<B.QuestionCategory> GetQuestionCategories(int id)
        //{
        //    IEnumerable<B.QuestionCategory> rtn = questionRepository.GetQuestionCategories(id).Select(qc => new B.QuestionCategory
        //    {
        //        Id = qc.QuestionCategory_ID_PK,
        //        QuestionId = qc.Question_ID_FK,
        //        CategoryId = qc.Category_ID_FK,
        //        Category = qc.Category.Name
        //    });

        //    return rtn;
        //}

        public B.Question AddQuestion(B.Question question)
        {
            var addedQuestion = questionRepository.Add(new E.Question { Question1 = question.Question1, Answer = question.Answer, RoundIdFk = question.RoundId, QuestionDifficulty = question.QuestionDifficulty });
            List<B.QuestionCategory> qc = new List<B.QuestionCategory>();
            List<B.Category> nonExistantCategories = new List<B.Category>();
            foreach (var item in question.Categories.Where(q => q.Id == 0))//non existant categories - add them
            {
                var nec = questionRepository.Add(new E.Category { Name = item.Name });
                nonExistantCategories.Add(new B.Category { Id = nec.CategoryIdPk, Name = nec.Name });
            }

            foreach (var item in question.Categories.Where(q => q.Id != 0))
            {
                var qc1 = questionRepository.Add(new E.QuestionCategory { QuestionIdFk = addedQuestion.QuestionIdPk, CategoryIdFk = item.Id });
            }

            foreach (var item in nonExistantCategories)
            {
                var qc1 = questionRepository.Add(new E.QuestionCategory { QuestionIdFk = addedQuestion.QuestionIdPk, CategoryIdFk = item.Id });
            }
            B.Question rtn = new B.Question
            {
                Id = addedQuestion.QuestionIdPk,
                RoundId = addedQuestion.RoundIdFk,
                Question1 = addedQuestion.Question1,
                Answer = addedQuestion.Answer,
                QuestionDifficulty = addedQuestion.QuestionDifficulty
            };
            return rtn;
        }

        public B.Question EditQuestion(B.Question question)
        {
            var addedQuestion = questionRepository.Update(new E.Question
            {
                QuestionIdPk = question.Id,
                Question1 = question.Question1,
                Answer = question.Answer,
                RoundIdFk = question.RoundId,
                QuestionDifficulty = question.QuestionDifficulty
            });

            List<B.QuestionCategory> qc = new List<B.QuestionCategory>();
            List<B.Category> nonExistantCategories = new List<B.Category>();
            foreach (var item in question.Categories.Where(q => q.Id == 0))//non existant categories - add them
            {
                var nec = questionRepository.Add(new E.Category { Name = item.Name });
                nonExistantCategories.Add(new B.Category { Id = nec.CategoryIdPk, Name = nec.Name });
            }

            var existingQuestionCategories = questionRepository.GetQuestionCategories(question.Id);

            foreach (var item in question.Categories.Where(q => q.Id != 0))
            {
                if (existingQuestionCategories.Where(eqc => eqc.CategoryIdFk == item.Id).Count() == 0)
                {
                    var qc1 = questionRepository.Add(new E.QuestionCategory { QuestionIdFk = addedQuestion.QuestionIdPk, CategoryIdFk = item.Id });
                }

            }

            foreach (var item in existingQuestionCategories)
            {
                if (question.Categories.Where(c => c.Id == item.CategoryIdFk).Count() == 0)
                {
                    questionRepository.Delete(item.QuestionCategoryIdPk);
                }
            }

            foreach (var item in nonExistantCategories)
            {
                var qc1 = questionRepository.Add(new E.QuestionCategory { QuestionIdFk = addedQuestion.QuestionIdPk, CategoryIdFk = item.Id });
            }

            B.Question rtn = new B.Question
            {
                Id = addedQuestion.QuestionIdPk,
                RoundId = addedQuestion.RoundIdFk,
                Question1 = addedQuestion.Question1,
                Answer = addedQuestion.Answer
            };
            return rtn;
        }

        public B.Question GetQuestion(int QuestionId)
        {
            var question = questionRepository.Get(QuestionId);
            var questionCategories = questionRepository.GetCategoriesForQuestion(QuestionId).Select(qc => new B.Category
            {
                Id = qc.CategoryIdFk,
                Name = qc.CategoryIdFkNavigation.Name
            }).ToList();

            B.Question rtn = new B.Question
            {
                Id = question.QuestionIdPk,
                RoundId = question.RoundIdFk,
                Question1 = question.Question1,
                Answer = question.Answer,
                Categories = questionCategories,
                QuestionDifficulty = question.QuestionDifficulty
            };
            return rtn;
        }

        //public IEnumerable<B.Question> GetQuestions()
        //{
        //    //IEnumerable<B.Question> rtn = questionRepository.All().Select(c => new B.Question { Id = c.Question_ID_PK, Question1 = c.Question1, Answer = c.Answer, QuestionDifficulty = c.QuestionDifficulty });
        //    //return rtn;

        //    IEnumerable<B.Question> rtn = questionRepository.All().Select(q => new B.Question
        //    {
        //        Id = q.Question_ID_PK,
        //        RoundId = q.Round_ID_FK,
        //        Question1 = q.Question1,
        //        Categories = q.QuestionCategories.Select(c => new B.Category { Id = c.Category.Category_ID_PK, Name = c.Category.Name}).ToList(),
        //        Answer = q.Answer,
        //        QuestionDifficulty = q.QuestionDifficulty
        //    });

        //    return rtn;

        //}

        //public B.RoundResult GetRoundResult(int roundResultId)
        //{
        //    var roundResult =  roundRepository.GetRoundResult(roundResultId);
        //    B.RoundResult rr = new B.RoundResult
        //    {
        //        Id = roundResult.RoundResult_ID_PK,
        //        RoundId = roundResult.Round_ID_FK,
        //        ContestantId = roundResult.Contestant_ID_FK,
        //        Contestant = roundResult.Contestant.Name,
        //        Points1 = roundResult.Points1,
        //        Points2 = roundResult.Points2,
        //        Points3 = roundResult.Points3,
        //    };
        //    return rr;
        //}

        //public B.RoundResult AddRoundResult(B.RoundResult roundResult)
        //{
        //    var addedRoundResult = roundRepository.AddRoundResult(new E.RoundResult {
        //        RoundResult_ID_PK = roundResult.Id,
        //        Round_ID_FK = roundResult.RoundId,
        //        Contestant_ID_FK = roundResult.ContestantId,
        //        Points1 = roundResult.Points1,
        //        Points2 = roundResult.Points2,
        //        Points3 = roundResult.Points3
        //    });

        //    B.RoundResult rtn = new B.RoundResult
        //    {
        //        Id = addedRoundResult.RoundResult_ID_PK,
        //        RoundId = addedRoundResult.Round_ID_FK,
        //        ContestantId = addedRoundResult.Contestant_ID_FK,
        //        Points1 = addedRoundResult.Points1,
        //        Points2 = addedRoundResult.Points2,
        //        Points3 = addedRoundResult.Points3,
        //    };

        //    return rtn;
        //}

        //public B.RoundResult EditRoundResult(B.RoundResult roundResult)
        //{
        //    var updatedRoundResult = roundRepository.UpdateRoundResult(new E.RoundResult
        //    {
        //        RoundResult_ID_PK = roundResult.Id,
        //        Round_ID_FK = roundResult.RoundId,
        //        Contestant_ID_FK = roundResult.ContestantId,
        //        Points1 = roundResult.Points1,
        //        Points2 = roundResult.Points2,
        //        Points3 = roundResult.Points3
        //    });

        //    B.RoundResult rtn = new B.RoundResult
        //    {
        //        Id = updatedRoundResult.RoundResult_ID_PK,
        //        RoundId = updatedRoundResult.Round_ID_FK,
        //        ContestantId = updatedRoundResult.Contestant_ID_FK,
        //        Contestant = updatedRoundResult.Contestant.Name,
        //        Points1 = updatedRoundResult.Points1,
        //        Points2 = updatedRoundResult.Points2,
        //        Points3 = updatedRoundResult.Points3,
        //    };

        //    return rtn;
        //}

        //public B.RoundResult DeleteRoundResult(int roundResultId)
        //{
        //    var roundResult = roundRepository.DeleteRoundResult(roundResultId);
        //    B.RoundResult rr = new B.RoundResult
        //    {
        //        Id = roundResult.RoundResult_ID_PK,
        //        RoundId = roundResult.Round_ID_FK,
        //        ContestantId = roundResult.Contestant_ID_FK,

        //        Points1 = roundResult.Points1,
        //        Points2 = roundResult.Points2,
        //        Points3 = roundResult.Points3,
        //    };
        //    return rr;
        //}

        //public IEnumerable<B.RoundResult> GetRoundResultsForRound(int RoundId)
        //{

        //    IEnumerable<B.RoundResult> rtn = roundRepository.GetRoundResultsForRound(RoundId).Select(q => new B.RoundResult
        //    {
        //        Id = q.RoundResult_ID_PK,
        //        RoundId = q.Round_ID_FK,
        //        ContestantId = q.Contestant_ID_FK,
        //        Contestant = q.Contestant.Name,
        //        Points1 = q.Points1,
        //        Points2 = q.Points2,
        //        Points3 = q.Points3
        //    });

        //    return rtn;
        //}

        ////QuizResults
        public IEnumerable<B.QuizResult> GetQuizResults(int QuizId)
        {
            IEnumerable<B.QuizResult> rtn = quizRepository.GetQuizResults(QuizId).Select(qr => new B.QuizResult
            {
                Id = qr.QuizResultIdPk,
                QuizId = qr.QuizIdFk,
                ContestantId = qr.ContestantIdFk,
                Contestant = qr.ContestantIdFkNavigation.Name,

                Round1Points = qr.Round1Points,
                PointsAfterRound1 = qr.PointsAfterRound1,
                RoundResult1Id = qr.RoundResult1IdFk,

                Round2Points = qr.Round2Points,
                PointsAfterRound2 = qr.PointsAfterRound2,
                RoundResult2Id = qr.RoundResult2IdFk,

                Round3Points = qr.Round3Points,
                PointsAfterRound3 = qr.PointsAfterRound3,
                RoundResult3Id = qr.RoundResult3IdFk,

                Round4Points = qr.Round4Points,
                PointsAfterRound4 = qr.PointsAfterRound4,
                RoundResult4Id = qr.RoundResult4IdFk
            });

            return rtn;
        }

        //public IEnumerable<B.QuizResult> RefreshQuizResults(int QuizId)
        //{
        //    IEnumerable<B.QuizResult> rtn = quizRepository.RefreshQuizResults(QuizId).Select(qr => new B.QuizResult
        //    {
        //        Id = qr.QuizResult_ID_PK,
        //        QuizId = qr.Quiz_ID_FK,
        //        ContestantId = qr.Contestant_ID_FK,

        //        Round1Points = qr.Round_1_Points,
        //        PointsAfterRound1 = qr.PointsAfterRound_1,
        //        RoundResult1Id = qr.RoundResult_1_ID_FK,

        //        Round2Points = qr.Round_2_Points,
        //        PointsAfterRound2 = qr.PointsAfterRound_2,
        //        RoundResult2Id = qr.RoundResult_2_ID_FK,

        //        Round3Points = qr.Round_3_Points,
        //        PointsAfterRound3 = qr.PointsAfterRound_3,
        //        RoundResult3Id = qr.RoundResult_3_ID_FK,

        //        Round4Points = qr.Round_4_Points,
        //        PointsAfterRound4 = qr.PointsAfterRound_4,
        //        RoundResult4Id = qr.RoundResult_4_ID_FK
        //    });

        //    return rtn;
        //}

        public IEnumerable<B.QuizResult> AddQuizResults(IEnumerable<B.QuizResult> QuizResults)
        {
            List<B.QuizResult> rtn = new List<B.QuizResult>();

            foreach (var result in QuizResults)
            {
                var contestantId = contestantRepository.GetByName(result.Contestant).ContestantIdPk;
                var res = quizRepository.AddQuizResult(new E.QuizResult
                {
                    //QuizResult_ID_PK = result.Id,
                    ContestantIdFk = contestantId,
                    QuizIdFk = result.QuizId,
                    Round1Points = result.Round1Points,
                    Round2Points = result.Round2Points,
                    Round3Points = result.Round3Points,
                    Round4Points = result.Round4Points,
                    PointsAfterRound1 = result.PointsAfterRound1,
                    PointsAfterRound2 = result.PointsAfterRound2,
                    PointsAfterRound3 = result.PointsAfterRound3,
                    PointsAfterRound4 = result.PointsAfterRound4
                });

                rtn.Add(new B.QuizResult
                {
                    Id = res.QuizResultIdPk,
                    Round1Points = res.Round1Points,
                    Round2Points = res.Round2Points,
                    Round3Points = res.Round3Points,
                    Round4Points = res.Round4Points,
                    PointsAfterRound1 = res.PointsAfterRound1,
                    PointsAfterRound2 = res.PointsAfterRound2,
                    PointsAfterRound3 = res.PointsAfterRound3,
                    PointsAfterRound4 = res.PointsAfterRound4
                });
            }
            return rtn;
        }

        public IEnumerable<B.QuizResult> UpdateQuizResults(IEnumerable<B.QuizResult> QuizResults)
        {
            List<B.QuizResult> rtn = new List<B.QuizResult>();

            foreach (var result in QuizResults)
            {
                var res = quizRepository.UpdateQuizResult(new E.QuizResult
                {
                    QuizResultIdPk = result.Id,
                    Round1Points = result.Round1Points,
                    Round2Points = result.Round2Points,
                    Round3Points = result.Round3Points,
                    Round4Points = result.Round4Points,
                    PointsAfterRound1 = result.PointsAfterRound1,
                    PointsAfterRound2 = result.PointsAfterRound2,
                    PointsAfterRound3 = result.PointsAfterRound3,
                    PointsAfterRound4 = result.PointsAfterRound4
                });

                rtn.Add(new B.QuizResult
                {
                    Id = res.QuizResultIdPk,
                    Round1Points = res.Round1Points,
                    Round2Points = res.Round2Points,
                    Round3Points = res.Round3Points,
                    Round4Points = res.Round4Points,
                    PointsAfterRound1 = res.PointsAfterRound1,
                    PointsAfterRound2 = res.PointsAfterRound2,
                    PointsAfterRound3 = res.PointsAfterRound3,
                    PointsAfterRound4 = res.PointsAfterRound4
                });
            }
            return rtn;
        }

        #region Contestant
        public IEnumerable<B.Contestant> GetContestantsForCompetition(int CompetitionId)
        {
            IEnumerable<B.Contestant> rtn = contestantRepository.GetContestantsForCompetition(CompetitionId).Select(q => new B.Contestant
            {
                Id = q.ContestantIdPk,
                CompetitionId = q.CompetitionIdFk,
                Name = q.Name
            });

            return rtn;
        }

        public B.Contestant GetContestant(int ContestantId)
        {
            var contestant = contestantRepository.Get(ContestantId);
            B.Contestant rtn = new B.Contestant { Id = contestant.ContestantIdPk, Name = contestant.Name, CompetitionId = contestant.CompetitionIdFk };
            return rtn;
        }

        public B.Contestant AddContestant(B.Contestant contestant)
        {
            var addedContestant = contestantRepository.Add(new E.Contestant { Name = contestant.Name, CompetitionIdFk = contestant.CompetitionId });
            B.Contestant rtn = new B.Contestant { Id = addedContestant.CompetitionIdFk, Name = addedContestant.Name, CompetitionId = addedContestant.CompetitionIdFk };
            return rtn;
        }

        public B.Contestant EditContestant(B.Contestant contestant)
        {
            var updatedContestant = contestantRepository.Update(new E.Contestant { ContestantIdPk = contestant.Id, Name = contestant.Name, CompetitionIdFk = contestant.CompetitionId });
            B.Contestant rtn = new B.Contestant { Id = updatedContestant.ContestantIdPk, Name = updatedContestant.Name, CompetitionId = updatedContestant.CompetitionIdFk };
            return rtn;
        }
        #endregion
    }
}