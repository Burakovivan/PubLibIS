using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.ViewModels
{
    public class BookViewModelSlim
    {
        public int Id { get; set; }
        public string Capation { get; set; }
        public string AuthorId { get; set; }
        public string AuthorFullName { get; set; }
    }
}
