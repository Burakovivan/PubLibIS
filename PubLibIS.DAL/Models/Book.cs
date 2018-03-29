using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PubLibIS.DAL.Models
{
    [Table("Books", Schema = "dbo")]
    public class Book : BaseEntity
    {
        public string ISBN { get; set; }
        public string Capation { get; set; }
        public string AdditionalData { get; set; }


        public virtual ICollection<PublishedBook> PublishedBooks { get; set; }
        public virtual ICollection<AuthorInBook> Authors { get; set; }
    }
}
