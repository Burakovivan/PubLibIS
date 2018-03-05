using System;
using System.Collections.Generic;

namespace PubLibIS.DAL.Models
{
    public class Periodical
    {
        public Periodical()
        {
            PeriodicalEditions = new List<PeriodicalEdition>();
        }

        public int Id { get; set; }
        public string ISSN { get; set; }
        public PeriodicalType Type { get; set; }
        public string Name { get; set; }
        public DateTime Foundation { get; set; }
        public bool IsPublished { get; set; }

        public virtual PublishingHouse PublishingHouse { get; set; }
        public virtual ICollection<PeriodicalEdition> PeriodicalEditions { get; set; }
    }
}
