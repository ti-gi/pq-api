using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pq_api.service.BusinessModels
{
    public class CompetitionCategoryCount
    {
        public int CompetitionId { get; set; }
        public int QuizId { get; set; }
        public string Category { get; set; }
        public int Count { get; set; }

    }
}
