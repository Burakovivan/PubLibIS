using Dapper.Contrib.Extensions;
using PubLibIS.Domain.Enums;
using System;
using System.Collections.Generic;

namespace PubLibIS.Domain.Entities
{
    [Table("Periodicals")]
    public class Periodical : BaseEntity
    {

        public string ISSN { get; set; }
        public PeriodicalType Type { get; set; }
        public string Name { get; set; }
        public DateTime Foundation { get; set; }
        public bool IsPublished { get; set; }
        public int? PublishingHouse_Id { get; set; }

        [Write(false)]
        public virtual PublishingHouse PublishingHouse { get; set; }
      
    }
}
