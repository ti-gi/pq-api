using System;
using System.Collections.Generic;

#nullable disable

namespace pq_api.data.Entities
{
    public partial class Question
    {
        public Question()
        {
            QuestionCategories = new HashSet<QuestionCategory>();
        }

        public int QuestionIdPk { get; set; }
        public string Question1 { get; set; }
        public string Answer { get; set; }
        public int? RoundIdFk { get; set; }
        public int QuestionDifficulty { get; set; }
        public string UserId { get; set; }
        public int? Ord { get; set; }

        public virtual Round RoundIdFkNavigation { get; set; }
        public virtual ICollection<QuestionCategory> QuestionCategories { get; set; }
    }
}
