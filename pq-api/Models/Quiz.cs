using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pq_api.Models
{
    public class Quiz
    {
        public int id { get; set; }
        public int competitionId { get; set; }
        public string name { get; set; }
    }
}