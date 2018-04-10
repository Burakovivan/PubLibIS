using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PubLibIS.Domain.Entities
{
    [Dapper.Contrib.Extensions.Table("PeriodicalEditions")]
    public class PeriodicalEdition : BaseEntity
    {
    
        public int ReleaseNumber { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Circulation { get; set; }//тираж
        public int Periodical_Id { get; set; }

        [ForeignKey("Periodical_Id")]
        [Write(false)]
        public virtual Periodical Periodical { get; set; }
    }
}
