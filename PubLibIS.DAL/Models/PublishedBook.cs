using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PubLibIS.DAL.Models
{
    public class PublishedBook
    {
        public int Id { get; set; }
        public int Volume { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateOfPublication { get; set; }
        public int? PublishingHouse_Id {get; set; }
        public int? Book_Id { get; set; }

        [ForeignKey("Book_Id")]
        public virtual Book Book { get; set; }
        [ForeignKey("PublishingHouse_Id")]
        public virtual PublishingHouse PublishingHouse { get; set; }
    }
}
