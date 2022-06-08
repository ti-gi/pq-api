using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pq_api.Models
{
    public class CompetitionCategoryCount
    {
        public int competitionId { get; set; }
        public int quizId { get; set; }
        public string category { get; set; }
        public int count{ get; set; }
    }
}