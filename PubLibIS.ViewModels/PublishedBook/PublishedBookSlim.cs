using System;
using System.ComponentModel.DataAnnotations;

namespace PubLibIS.ViewModels
{
    public class PublishedBookViewModelslim
    {
        public int Id { get; set; }
        public int Volume { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfPublication { get; set; }
    }
}
