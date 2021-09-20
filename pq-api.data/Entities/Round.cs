using System;
using System.Collections.Generic;

#nullable disable

namespace pq_api.data.Entities
{
    public partial class Round
    {
        public Round()
        {
            Questions = new HashSet<Question>();
        }

        public int RoundIdPk { get; set; }
        public string Name { get; set; }
        public int QuizIdFk { get; set; }
        public int RoundNumber { get; set; }

        public virtual Quiz QuizIdFkNavigation { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
