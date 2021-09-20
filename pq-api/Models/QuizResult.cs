using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pq_api.Models
{
    public class QuizResult
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public int ContestantId { get; set; }
        public string Contestant { get; set; }

        public decimal? Round1Points { get; set; }
        public decimal? PointsAfterRound1 { get; set; }
        public int? RoundResult1Id { get; set; }

        public decimal? Round2Points { get; set; }
        public decimal? PointsAfterRound2 { get; set; }
        public int? RoundResult2Id { get; set; }

        public decimal? Round3Points { get; set; }
        public decimal? PointsAfterRound3 { get; set; }
        public int? RoundResult3Id { get; set; }

        public decimal? Round4Points { get; set; }
        public decimal? PointsAfterRound4 { get; set; }
        public int? RoundResult4Id { get; set; }
    }
}