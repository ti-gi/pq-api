using System;
using System.Collections.Generic;

#nullable disable

namespace pq_api.data.Entities
{
    public partial class Competition
    {
        public Competition()
        {
            Contestants = new HashSet<Contestant>();
            Quizzes = new HashSet<Quiz>();
        }

        public int CompetitionIdPk { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Contestant> Contestants { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
}
