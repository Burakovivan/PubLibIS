using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PubLibIS.DAL.Models
{
    [Table("PeriodicalEditions", Schema = "dbo")]
    public class PeriodicalEdition : BaseEntity
    {
    
        public int ReleaseNumber { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Circulation { get; set; }//тираж
        public int Periodical_Id { get; set; }

        [ForeignKey("Periodical_Id")]
        public virtual Periodical Periodical { get; set; }
    }
}
