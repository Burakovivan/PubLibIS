using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class PublishingHouseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FoundationDate { get; set; }

        public string FullAddresFormated
        {
            get
            {
                return $"{Address}. {Country}, {City}, {PostalCode}";
            }
        }

        public string ListBoxInfo
        {
            get
            {
                return $"{Name} ({Country}, {City})";
            }
        }
    }
}
