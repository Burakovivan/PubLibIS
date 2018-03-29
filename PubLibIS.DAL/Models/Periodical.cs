using PubLibIS.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PubLibIS.DAL.Models
{
    [Table("Periodicals", Schema = "dbo")]
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

        public virtual PublishingHouse PublishingHouse { get; set; }
        public virtual ICollection<PeriodicalEdition> PeriodicalEditions { get; set; }
    }
}
