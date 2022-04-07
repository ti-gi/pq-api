using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pq_api.data.Entities
{
    public partial class CompetitionCategoryCount
    {
        public int CompetitionId { get; set; }
        public string Category { get; set; }
        public int Count { get; set; }

    }
}
