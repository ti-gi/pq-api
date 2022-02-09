using System;
using System.Collections.Generic;

#nullable disable

namespace pq_api.data.Entities
{
    public partial class Category
    {
        public Category()
        {
            QuestionCategories = new HashSet<QuestionCategory>();
        }

        public int CategoryIdPk { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }

        public virtual ICollection<QuestionCategory> QuestionCategories { get; set; }
    }
}
