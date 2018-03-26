using PubLibIS.ViewModels.Attributes;
using System;
using System.Web.Mvc;

namespace PubLibIS.ViewModels
{
    public class BrochureViewModel
    {
        public int Id { get; set; }
        public string Capation { get; set; }
        public int Volume { get; set; }
        [CustomDataDisplayFormat]
        public DateTime ReleaseDate { get; set; }
        public int Circulation { get; set; }//тираж


        public PublishingHouseViewModel PublishingHouse { get; set; }
        public SelectList PublishingHouseSelectList { get; set; }
        public int? PublishingHouse_Id { get; set; }
    }
}
