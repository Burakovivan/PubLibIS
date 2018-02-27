using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS_DAL.Model
{
    public class AuthorInBook
    {
        public int Id { get; set; }
        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
    }
}
