using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.DAL.Model
{
    public class AuthorInBook
    {
        public int Id { get; set; }
        [Required]
        public virtual Author Author { get; set; }
        [Required]
        public virtual Book Book { get; set; }
    }
}
