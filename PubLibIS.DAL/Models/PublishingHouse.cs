﻿using System;
using System.Collections.Generic;

namespace PubLibIS.DAL.Models
{
    public class PublishingHouse
    {
        public PublishingHouse()
        {
            Books = new List<PublishedBook>();
            Periodicals = new List<Periodical>();
            Brochures = new List<Brochure>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PostalCode { get; set; }
        public DateTime FoundationDate { get; set; }

        public virtual ICollection<PublishedBook> Books { get; set; }
        public virtual ICollection<Periodical> Periodicals { get; set; }
        public virtual ICollection<Brochure> Brochures { get; set; }
    }
}
