using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pq_api.Models
{
    public class RoundUpdate
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string Name { get; set; }
    }
}