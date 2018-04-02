using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace PubLibIS.DAL.Models
{
    [Table("Books")]
    public class Book : BaseEntity
    {
        public string ISBN { get; set; }
        public string Capation { get; set; }
        public string AdditionalData { get; set; }


        [Write(false)]
        public virtual ICollection<PublishedBook> PublishedBooks { get; set; }
        [Write(false)]
        public virtual ICollection<AuthorInBook> Authors { get; set; }
    }
}
