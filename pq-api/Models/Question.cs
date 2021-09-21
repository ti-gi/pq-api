using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pq_api.Models
{
    public class Question
    {
        public int id { get; set; }
        public int roundId { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public List<Category> Categories { get; set; }
        public int QuestionDifficulty { get; set; }
    }
}