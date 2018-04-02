using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dapper;
using Dapper.Contrib.Extensions;
namespace PubLibIS.DAL.Models
{
    [Table("Authors")]
    public class Author : BaseEntity
    {
        public Author()
        {
            Books = new List<AuthorInBook>();
        }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateOfBirth { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateOfDeath { get; set; }
       
        [Write(false)]
        public virtual ICollection<AuthorInBook> Books { get; set; }
    }
}
