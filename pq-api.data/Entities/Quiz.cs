using System;
using System.Collections.Generic;

#nullable disable

namespace pq_api.data.Entities
{
    public partial class Quiz
    {
        public Quiz()
        {
            QuizResults = new HashSet<QuizResult>();
            Rounds = new HashSet<Round>();
        }

        public int QuizIdPk { get; set; }
        public int CompetitionIdFk { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }

        public virtual Competition CompetitionIdFkNavigation { get; set; }
        public virtual ICollection<QuizResult> QuizResults { get; set; }
        public virtual ICollection<Round> Rounds { get; set; }
    }
}
