using System;
using System.Collections.Generic;

namespace PubLibIS.DAL.Models
{
    public class PeriodicalEdition
    {
        public PeriodicalEdition()
        {
        }
        public int Id { get; set; }
        public int ReleaseNumber { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Circulation { get; set; }//тираж

            
        public virtual Periodical Periodical { get; set; }
    }
}
