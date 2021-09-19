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
        }

        public int QuizIdPk { get; set; }
        public int CompetitionIdFk { get; set; }
        public string Name { get; set; }

        public virtual Competition CompetitionIdFkNavigation { get; set; }
        public virtual ICollection<QuizResult> QuizResults { get; set; }
    }
}
