using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Capation { get; set; }


        public AuthorViewModel Author { get; set; }
        public PeriodicalEditionViewModel PeriodicalEdition { get; set; }
    }
}
