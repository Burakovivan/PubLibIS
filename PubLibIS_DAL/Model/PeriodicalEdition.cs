using System;
using System.Collections.Generic;

namespace PubLibIS_DAL.Model
{
    public class PeriodicalEdition
    {
        public PeriodicalEdition()
        {
            Articles = new List<Article>();
        }
        public int Id { get; set; }
        public int ReleaseNumber { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Circulation { get; set; }//тираж

            
        public virtual Periodical Periodical { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
