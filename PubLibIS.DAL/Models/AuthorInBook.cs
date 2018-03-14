using System.ComponentModel.DataAnnotations;

namespace PubLibIS.DAL.Models
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
