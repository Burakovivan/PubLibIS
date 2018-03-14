using PubLibIS.ViewModels.Attributes;
using System;

namespace PubLibIS.ViewModels
{
    public class BookViewModelSlim
    {
        public int Id { get; set; }
        public string Capation { get; set; }
        public string Authors { get; set; }
        public int CountOfPublication { get; set; }
        [CustomDataDisplayFormat]
        public DateTime ReleaseDate { get; set; }
    }
}
