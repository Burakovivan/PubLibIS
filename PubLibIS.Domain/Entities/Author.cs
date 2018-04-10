using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dapper;
using Dapper.Contrib.Extensions;
namespace PubLibIS.Domain.Entities
{
    [Table("Authors")]
    public class Author : BaseEntity
    {
        
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Patronymic { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateOfBirth { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateOfDeath { get; set; }
       
    }
}
