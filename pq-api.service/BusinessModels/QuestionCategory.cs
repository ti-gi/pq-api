using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pq_api.service.BusinessModels
{
    public class QuestionCategory
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
    }
}