using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pq_api.service.BusinessModels
{
    public class RoundResult
    {
        public int Id { get; set; }
        public int RoundId { get; set; }
        public int ContestantId { get; set; }
        public string Contestant { get; set; }
        public decimal? Points1 { get; set; }
        public decimal? Points2 { get; set; }
        public decimal? Points3 { get; set; }
    }
}