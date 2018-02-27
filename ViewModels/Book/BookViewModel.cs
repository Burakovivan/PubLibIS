using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string ISBN { get; set; }
        public string Capation { get; set; }
        public string AdditionalData { get; set; }

        public List<AuthorViewModel> Authors { get; set; }


        public string ReleaseDateFormated
        {
            get
            {
                if (ReleaseDate != null && ReleaseDate.HasValue)
                    return $"{ReleaseDate:dd.MM.yyyy}";
                else return "No releases";
            }
        }
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
