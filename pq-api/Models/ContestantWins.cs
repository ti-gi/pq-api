using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pq_api.Models
{
    public class ContestantWins
    {
        public int competitionId { get; set; }
        public string contestant { get; set; }
        public int wins { get; set; }
    }
}