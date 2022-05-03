using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pq_api.Models
{
    public class QuestionCreate
    {
        public int? RoundId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public List<Category> Categories { get; set; }
        public int QuestionDifficulty { get; set; }
    }
}