using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PubLibIS.ViewModels
{
    public class PublishedBookViewModel
    {
        public int Id { get; set; }
        [Required]
        public int Volume { get; set; }
        [Required(AllowEmptyStrings = true)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfPublication { get; set; }

        public BookViewModel Book { get; set; }
        public PublishingHouseViewModel PublishingHouse { get; set; }
        public SelectList PublishingHouseSelectList { get; set; }
    }
}
