using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace PubLibIS.Domain.Entities
{
    [Table("PublishingHouses")]
    public class PublishingHouse : BaseEntity
    {
        public PublishingHouse()
        {
        }

        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public DateTime FoundationDate { get; set; }
    }

}
