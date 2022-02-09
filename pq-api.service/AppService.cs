﻿using System;
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
        public IEnumerable<B.Competition> GetAllCompetitions(string userId)
        {
            IEnumerable<B.Competition> rtn = competitionRepository.GetCompetitions(userId)
                .Select(c => new B.Competition { Id = c.CompetitionIdPk, Name = c.Name });

            return rtn;
        }

        public B.Competition GetCompetition(string userId, int CompetitionId)
        {
            var competition = competitionRepository.Get(userId, CompetitionId);
            B.Competition rtn = new B.Competition { Id = competition.CompetitionIdPk, Name = competition.Name };
            return rtn;
        }

        public B.Competition AddCompetition(string userId, B.Competition competition)
        {
            var addedCompetition = competitionRepository.Add(new E.Competition { Name = competition.Name, UserId = userId});
            B.Competition rtn = new B.Competition { Id = addedCompetition.CompetitionIdPk, Name = addedCompetition.Name };
            return rtn;
        }

        public B.Competition UpdateCompetition(string userId, B.Competition competition)
        {
            var updatedCompetition = competitionRepository.Update(new E.Competition { CompetitionIdPk = competition.Id, Name = competition.Name, UserId = userId});
            B.Competition rtn = new B.Competition { Id = updatedCompetition.CompetitionIdPk, Name = updatedCompetition.Name };
            return rtn;
        }

        public IEnumerable<B.Contestant> GetContestantsForCompetition(string userId, int CompetitionId)
        {
            IEnumerable<B.Contestant> rtn = contestantRepository.GetContestantsForCompetition(userId, CompetitionId).Select(q => new B.Contestant
            {
                Id = q.ContestantIdPk,
                CompetitionId = q.CompetitionIdFk,
                Name = q.Name
            });

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

        #region Contestant

        public B.Contestant GetContestant(string userId, int ContestantId)
        {
            var contestant = contestantRepository.Get(userId, ContestantId);
            B.Contestant rtn = new B.Contestant { Id = contestant.ContestantIdPk, Name = contestant.Name, CompetitionId = contestant.CompetitionIdFk };
            return rtn;
        }

        public Response<B.Contestant> AddContestant(string userId, B.Contestant contestant)
        {
            if (contestantRepository.GetByName(userId, contestant.Name) != null)
            {
                return new Response<B.Contestant>
                {
                    Data = null,
                    Message = "Contestant with the same name already exists! Please select different name."
                };
            }
            var addedContestant = contestantRepository.Add(new E.Contestant
            {
                Name = contestant.Name,
                CompetitionIdFk = contestant.CompetitionId,
                UserId = userId
            });

            B.Contestant rtn = new B.Contestant
            {
                Id = addedContestant.ContestantIdPk,
                Name = addedContestant.Name,
                CompetitionId = addedContestant.CompetitionIdFk
            };

            return new Response<B.Contestant>
            {
                Data = rtn,
                Message = ""
            };
        }

        public Response<B.Contestant> EditContestant(string userId, B.Contestant contestant)
        {
            if (contestantRepository.GetByName(userId, contestant.Name) != null)
            {
                return new Response<B.Contestant>
                {
                    Data = null,
                    Message = "Contestant with the same name already exists! Please select different name."
                };
            }

            var updatedContestant = contestantRepository.Update(new E.Contestant
            {
                ContestantIdPk = contestant.Id,
                Name = contestant.Name,
                CompetitionIdFk = contestant.CompetitionId,
                UserId = userId
            });

            B.Contestant rtn = new B.Contestant
            {
                Id = updatedContestant.ContestantIdPk,
                Name = updatedContestant.Name,
                CompetitionId = updatedContestant.CompetitionIdFk
            };

            return new Response<B.Contestant>
            {
                Data = rtn,
                Message = ""
            };
        }

        public Response<B.Contestant> DeleteContestant(string userId, int id, bool deleteConfirmed)
        {
            if (!deleteConfirmed)
            {
                if (quizRepository.GetQuizResults().Where(qr => qr.ContestantIdFk == id).Count() > 0)
                {
                    return new Response<B.Contestant>
                    {
                        Data = null,
                        Message = "Also delete results related to selected contestant?"
                    };
                }
                else
                {
                    return new Response<B.Contestant>
                    {
                        Data = null,
                        Message = "Are you sure you want to delete selected contestant?"
                    };
                }
            }

            else
            {
                //delete related results
                var quizResultsForContestant = quizRepository.GetQuizResults().Where(qr => qr.ContestantIdFk == id);
                foreach (var item in quizResultsForContestant)
                {
                    quizRepository.DeleteQuizResult(item.QuizResultIdPk);
                }

                //delete contestant
                var deletedContestant = contestantRepository.Delete(userId, id);

                B.Contestant rtn = new B.Contestant
                {
                    Id = deletedContestant.ContestantIdPk,
                    Name = deletedContestant.Name,
                    CompetitionId = deletedContestant.CompetitionIdFk
                };

                return new Response<B.Contestant>
                {
                    Data = rtn,
                    Message = ""
                };

            }

        }
        #endregion

        #region Quiz

        public IEnumerable<B.Quiz> GetQuizzes()
        {
            string userId = "";
            IEnumerable<B.Quiz> rtn = quizRepository.GetQuizzes(userId).Select(q => new B.Quiz
            {
                Id = q.QuizIdPk,
                CompetitionId = q.CompetitionIdFk,
                Name = q.Name
            });

            return rtn;
        }
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

        public IEnumerable<B.Round> GetRounds()
        {
            string userId = "";
            IEnumerable<B.Round> rtn = roundRepository.All(userId).Select(q => new B.Round
            {
                Id = q.RoundIdPk,
                QuizId = q.QuizIdFk,
                Name = q.Name
            });

            return rtn;
        }
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

        #region Questions
        public IEnumerable<B.Question> GetQuestionsForRound(string userId, int roundId)
        {
            IEnumerable<B.Question> rtn = questionRepository.GetQuestionsForRound(userId, roundId).Select(q => new B.Question
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

        public B.Question AddQuestion(string userId, B.Question question)
        {
            var addedQuestion = questionRepository.Add(new E.Question { 
                Question1 = question.Question1, 
                Answer = question.Answer, 
                RoundIdFk = question.RoundId, 
                QuestionDifficulty = question.QuestionDifficulty, 
                UserId = userId 
            });

            List<B.QuestionCategory> qc = new List<B.QuestionCategory>();
            List<B.Category> nonExistantCategories = new List<B.Category>();
            foreach (var item in question.Categories.Where(q => q.Id == 0))//non existant categories - add them
            {
                var nec = questionRepository.Add(new E.Category { Name = item.Name, UserId = userId });
                nonExistantCategories.Add(new B.Category { Id = nec.CategoryIdPk, Name = nec.Name });
            }

            foreach (var item in question.Categories.Where(q => q.Id != 0))
            {
                var qc1 = questionRepository.Add(new E.QuestionCategory { QuestionIdFk = addedQuestion.QuestionIdPk, CategoryIdFk = item.Id, UserId = userId });
            }

            foreach (var item in nonExistantCategories)
            {
                var qc1 = questionRepository.Add(new E.QuestionCategory { QuestionIdFk = addedQuestion.QuestionIdPk, CategoryIdFk = item.Id, UserId = userId });
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

        public B.Question EditQuestion(string userId, B.Question question)
        {
            var addedQuestion = questionRepository.Update(new E.Question
            {
                QuestionIdPk = question.Id,
                Question1 = question.Question1,
                Answer = question.Answer,
                RoundIdFk = question.RoundId,
                QuestionDifficulty = question.QuestionDifficulty,
                UserId = userId
            });

            List<B.QuestionCategory> qc = new List<B.QuestionCategory>();
            List<B.Category> nonExistantCategories = new List<B.Category>();
            foreach (var item in question.Categories.Where(q => q.Id == 0))//non existant categories - add them
            {
                var nec = questionRepository.Add(new E.Category { Name = item.Name, UserId = userId });
                nonExistantCategories.Add(new B.Category { Id = nec.CategoryIdPk, Name = nec.Name });
            }

            var existingQuestionCategories = questionRepository.GetQuestionCategories(userId, question.Id);

            foreach (var item in question.Categories.Where(q => q.Id != 0))
            {
                if (existingQuestionCategories.Where(eqc => eqc.CategoryIdFk == item.Id).Count() == 0)
                {
                    var qc1 = questionRepository.Add(new E.QuestionCategory { QuestionIdFk = addedQuestion.QuestionIdPk, CategoryIdFk = item.Id, UserId = userId });
                }

            }

            foreach (var item in existingQuestionCategories)
            {
                if (question.Categories.Where(c => c.Id == item.CategoryIdFk).Count() == 0)
                {
                    questionRepository.Delete(userId, item.QuestionCategoryIdPk);
                }
            }

            foreach (var item in nonExistantCategories)
            {
                var qc1 = questionRepository.Add(new E.QuestionCategory { QuestionIdFk = addedQuestion.QuestionIdPk, CategoryIdFk = item.Id, UserId = userId });
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

        public IEnumerable<B.Category> GetCategories(string userId)
        {
            IEnumerable<B.Category> rtn = questionRepository.GetCategories(userId).Select(c => new B.Category
            {
                Id = c.CategoryIdPk,
                Name = c.Name
            });

            return rtn;
        }
        public B.Question GetQuestion(string userId, int QuestionId)
        {
            var question = questionRepository.Get(userId, QuestionId);
            var questionCategories = questionRepository.GetCategoriesForQuestion(userId, QuestionId).Select(qc => new B.Category
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

        #endregion

        
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

        public IEnumerable<B.QuizResult> AddQuizResults(string userId, IEnumerable<B.QuizResult> QuizResults)
        {
            List<B.QuizResult> rtn = new List<B.QuizResult>();

            foreach (var result in QuizResults)
            {
                var contestantId = contestantRepository.GetByName(userId, result.Contestant).ContestantIdPk;
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

        public B.QuizResult DeleteQuizResult(int id)
        {
            var res = quizRepository.DeleteQuizResult(id);
            return new B.QuizResult
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
            };
        }

        
    }
}