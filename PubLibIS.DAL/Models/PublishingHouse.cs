using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace PubLibIS.DAL.Models
{
    [Table("PublishingHouses")]
    public class PublishingHouse : BaseEntity
    {
        public PublishingHouse()
        {
            Books = new List<PublishedBook>();
            Periodicals = new List<Periodical>();
            Brochures = new List<Brochure>();
        }

        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public DateTime FoundationDate { get; set; }

        [Write(false)]
        public virtual ICollection<PublishedBook> Books { get; set; }
        [Write(false)]
        public virtual ICollection<Periodical> Periodicals { get; set; }
        [Write(false)]
        public virtual ICollection<Brochure> Brochures { get; set; }
    }
}
