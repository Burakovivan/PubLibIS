using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PubLibIS_DAL.Model
{
    public class Author
    {
        public Author()
        {
            Books = new List<Book>();
            Articles = new List<Article>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateOfDeath { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
    }
}
