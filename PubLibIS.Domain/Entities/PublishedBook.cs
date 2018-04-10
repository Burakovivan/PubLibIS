using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PubLibIS.Domain.Entities
{
    [Dapper.Contrib.Extensions.Table("PublishedBooks")]
    public class PublishedBook : BaseEntity
    {

        public int Volume { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateOfPublication { get; set; }
        public int? PublishingHouse_Id {get; set; }
        public int? Book_Id { get; set; }

        [ForeignKey("Book_Id")]
        [Write(false)]
        public virtual Book Book { get; set; }
        [ForeignKey("PublishingHouse_Id")]
        [Write(false)]
        public virtual PublishingHouse PublishingHouse { get; set; }
    }
}
