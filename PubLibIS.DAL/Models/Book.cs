using System;
using System.Collections.Generic;

namespace PubLibIS.DAL.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Capation { get; set; }
        public string AdditionalData { get; set; }


        public virtual ICollection<PublishedBook> PublishedBooks { get; set; }
        public virtual ICollection<AuthorInBook> Authors { get; set; }
    }
}
