using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.ViewModels
{
    public class PeriodicalCatalogViewModel
    {
        public IEnumerable<PeriodicalViewModel> Periodicals { get; set; }
        public int Skip { get; set; }
        public bool IsSeeMore { get; set; }
        public bool HasNextPage { get; set; }
    }
}
