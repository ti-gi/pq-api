using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pq_api.Models
{
    public class QuizUpdate
    {
        public int Id { get; set; }
        public int CompetitionId { get; set; }
        public string Name { get; set; }
    }
}