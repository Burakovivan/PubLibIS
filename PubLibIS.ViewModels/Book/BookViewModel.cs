using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ReleaseDate { get; set; }

        public string AuthorsFormated
        {
            get
            {
                return string.Join(", ", Authors.Select(x => x.FullName).ToArray());
            }
        }
    }
}
