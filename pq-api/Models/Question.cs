using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pq_api.Models
{
    public class Question
    {
        public int id { get; set; }
        public int? roundId { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public List<Category> categories { get; set; }
        public int questionDifficulty { get; set; }
        public int? ord { get; set; }
    }
}