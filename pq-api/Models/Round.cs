using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pq_api.Models
{
    public class Round
    {
        public int id { get; set; }
        public int quizId { get; set; }
        public string name { get; set; }
    }
}