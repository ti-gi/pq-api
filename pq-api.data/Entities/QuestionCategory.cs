using System;
using System.Collections.Generic;

#nullable disable

namespace pq_api.data.Entities
{
    public partial class QuestionCategory
    {
        public int QuestionCategoryIdPk { get; set; }
        public int QuestionIdFk { get; set; }
        public int CategoryIdFk { get; set; }

        public virtual Category CategoryIdFkNavigation { get; set; }
        public virtual Question QuestionIdFkNavigation { get; set; }
    }
}
