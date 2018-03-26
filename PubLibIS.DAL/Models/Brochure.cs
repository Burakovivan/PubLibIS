using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PubLibIS.DAL.Models
{
    public class Brochure
    {
        public int Id { get; set; }
        public string Capation { get; set; }
        public int Volume { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Circulation { get; set; }//тираж
        public int? PublishingHouse_Id { get; set; }
        
        public virtual PublishingHouse PublishingHouse { get; set; }
    }
}
