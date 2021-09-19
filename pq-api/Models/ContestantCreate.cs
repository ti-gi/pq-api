using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pq_api.Models
{
    public class ContestantCreate
    {
        public int CompetitionId { get; set; }
        public string Name { get; set; }
    }
}