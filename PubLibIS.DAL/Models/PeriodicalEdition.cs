using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PubLibIS.DAL.Models
{
    public class PeriodicalEdition
    {
        
        public int Id { get; set; }
        public int ReleaseNumber { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Circulation { get; set; }//тираж

        public virtual Periodical Periodical { get; set; }
    }
}
