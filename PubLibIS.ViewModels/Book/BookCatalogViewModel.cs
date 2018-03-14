using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.ViewModels
{
    public class BookCatalogViewModel
    {
        public IEnumerable<BookViewModelSlim> Books { get; set; }
        public int Skip { get; set; }
        public bool IsSeeMore { get; set; }
        public bool HasNextPage { get; set; }
    }
}
