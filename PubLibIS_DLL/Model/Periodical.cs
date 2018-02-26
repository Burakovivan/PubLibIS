using System;
using System.Collections.Generic;

namespace PubLibIS_BLL.Model
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

        public PublishingHouse PublishingHouse { get; set; }
        public List<PeriodicalEdition> PeriodicalEditions { get; set; }
    }
}
