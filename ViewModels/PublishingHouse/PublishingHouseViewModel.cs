using System;

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
        public string FoundationDate { get; set; }

        public string FullAddresFormated
        {
            get
            {
                return $"{Address}. {Country}, {City}, {PostalCode}";
            }
        }
        public string FoundationDateFormated
        {
            get
            {
                return $"{FoundationDate:dd.MM.yyyy}";
            }
        }
    }
}
