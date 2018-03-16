using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PubLibIS.DAL.Models
{
    public class AuthorInBook
    {
        public int Id { get; set; }

        public int Author_Id { get; set; } 
        public int Book_Id { get; set; } 

        [Required]
        [ForeignKey("Author_Id")]
        public virtual Author Author { get; set; }
        [Required]
        [ForeignKey("Book_Id")]
        public virtual Book Book { get; set; }
    }
}
