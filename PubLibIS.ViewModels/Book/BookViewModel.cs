using PubLibIS.ViewModels.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PubLibIS.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Capation { get; set; }
        public string AdditionalData { get; set; }

        public IEnumerable<AuthorViewModel> Authors { get; set; }
        public MultiSelectList AuthorsSelectList { get; set; }
        public IEnumerable<PublishedBookViewModel> Publications { get; set; }
        public int PublicationsCount => Publications == null?0:Publications.Count();

        [CustomDataDisplayFormat]
        public DateTime? ReleaseDate { get; set; }

        public string AuthorsFormated
        {
            get
            {
                return Authors != null? string.Join(", ", Authors.Select(x => x.FullName).ToArray()):"";
            }
        }
    }
}
