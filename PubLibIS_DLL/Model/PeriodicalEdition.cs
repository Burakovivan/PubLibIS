using System;
using System.Collections.Generic;

namespace PubLibIS_BLL.Model
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

            
        public  Periodical Periodical { get; set; }
        public  List<Article> Articles { get; set; }
    }
}
