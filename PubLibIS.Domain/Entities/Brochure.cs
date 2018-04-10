using Dapper.Contrib.Extensions;
using System;

namespace PubLibIS.Domain.Entities
{
    [Table("Brochures")]
    public class Brochure : BaseEntity
    {
        public string Capation { get; set; }
        public int Volume { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Circulation { get; set; }//тираж
        public int? PublishingHouse_Id { get; set; }

        [Write(false)]
        public virtual PublishingHouse PublishingHouse { get; set; }
    }
}
