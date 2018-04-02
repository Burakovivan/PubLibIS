using Dapper.Contrib.Extensions;
using PubLibIS.DAL.Enums;
using System;
using System.Collections.Generic;

namespace PubLibIS.DAL.Models
{
    [Table("Periodicals")]
    public class Periodical : BaseEntity
    {
        public Periodical()
        {
            PeriodicalEditions = new List<PeriodicalEdition>();
        }

        public string ISSN { get; set; }
        public PeriodicalType Type { get; set; }
        public string Name { get; set; }
        public DateTime Foundation { get; set; }
        public bool IsPublished { get; set; }
        public int? PublishingHouse_Id { get; set; }

        [Write(false)]
        public virtual PublishingHouse PublishingHouse { get; set; }
        [Write(false)]
        public virtual ICollection<PeriodicalEdition> PeriodicalEditions { get; set; }
    }
}
