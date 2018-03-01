using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class PeriodicalEditionViewModel
    {

        public int Id { get; set; }
        public int ReleaseNumber { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Circulation { get; set; }//тираж


        public PeriodicalViewModel Periodical { get; set; }
        public ICollection<ArticleViewModel> Articles { get; set; }
    }
}
