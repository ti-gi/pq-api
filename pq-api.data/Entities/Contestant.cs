using System;
using System.Collections.Generic;

#nullable disable

namespace pq_api.data.Entities
{
    public partial class Contestant
    {
        public Contestant()
        {
            QuizResults = new HashSet<QuizResult>();
        }

        public int ContestantIdPk { get; set; }
        public int CompetitionIdFk { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }

        public virtual Competition CompetitionIdFkNavigation { get; set; }
        public virtual ICollection<QuizResult> QuizResults { get; set; }
    }
}
