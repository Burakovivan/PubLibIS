using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PubLibIS.DAL.Models
{
    [Dapper.Contrib.Extensions.Table("AuthorInBooks")]
    public class AuthorInBook : BaseEntity
    {

        public int Author_Id { get; set; }
        public int Book_Id { get; set; }

        [Required]
        [ForeignKey("Author_Id")]
        [Write(false)]
        public virtual Author Author { get; set; }
        [Required]
        [ForeignKey("Book_Id")]
        [Write(false)]
        public virtual Book Book { get; set; }
    }
}
