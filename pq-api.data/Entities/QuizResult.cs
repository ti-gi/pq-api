using System;
using System.Collections.Generic;

#nullable disable

namespace pq_api.data.Entities
{
    public partial class QuizResult
    {
        public int QuizResultIdPk { get; set; }
        public int QuizIdFk { get; set; }
        public int ContestantIdFk { get; set; }
        public decimal? Round1Points { get; set; }
        public decimal? PointsAfterRound1 { get; set; }
        public int? RoundResult1IdFk { get; set; }
        public decimal? Round2Points { get; set; }
        public decimal? PointsAfterRound2 { get; set; }
        public int? RoundResult2IdFk { get; set; }
        public decimal? Round3Points { get; set; }
        public decimal? PointsAfterRound3 { get; set; }
        public int? RoundResult3IdFk { get; set; }
        public decimal? Round4Points { get; set; }
        public decimal? PointsAfterRound4 { get; set; }
        public int? RoundResult4IdFk { get; set; }
        public string UserId { get; set; }

        public virtual Contestant ContestantIdFkNavigation { get; set; }
        public virtual Quiz QuizIdFkNavigation { get; set; }
    }
}
