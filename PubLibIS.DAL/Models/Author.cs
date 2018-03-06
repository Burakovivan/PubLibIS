using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PubLibIS.DAL.Models
{
    public class Author
    {
        public Author()
        {
            Books = new List<AuthorInBook>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateOfBirth { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DateOfDeath { get; set; }

        public virtual ICollection<AuthorInBook> Books { get; set; }
    }
}
