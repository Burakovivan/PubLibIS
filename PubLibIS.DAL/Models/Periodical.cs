using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int? PublishingHouse_Id { get; set; }

        [ForeignKey("PublishingHouse_Id")]
        public virtual PublishingHouse PublishingHouse { get; set; }
        public virtual ICollection<PeriodicalEdition> PeriodicalEditions { get; set; }
    }
}
