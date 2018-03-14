using System;
using System.ComponentModel.DataAnnotations;

namespace PubLibIS.DAL.Models
{
    public class PublishedBook
    {
        public int Id { get; set; }
        public int Volume { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateOfPublication { get; set; }

        public virtual Book Book { get; set; }
        public virtual PublishingHouse PublishingHouse { get; set; }
    }
}
