using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class PublishedBookViewModelSlim
    {
        public int Id { get; set; }
        public int Volume { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfPublication { get; set; }
    }
}
