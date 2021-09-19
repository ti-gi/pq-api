using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pq_api.service.BusinessModels
{
    public class Round
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuizId { get; set; }
    }
}