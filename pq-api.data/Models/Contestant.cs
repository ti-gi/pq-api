using System;
using System.Collections.Generic;

#nullable disable

namespace pq_api.data.Models
{
    public partial class Contestant
    {
        public int ContestantIdPk { get; set; }
        public int CompetitionIdFk { get; set; }
        public string Name { get; set; }

        public virtual Competition CompetitionIdFkNavigation { get; set; }
    }
}
