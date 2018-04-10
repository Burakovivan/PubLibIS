using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace PubLibIS.Domain.Entities
{
    [Table("Books")]
    public class Book : BaseEntity
    {
        public string ISBN { get; set; }
        public string Capation { get; set; }
        public string AdditionalData { get; set; }
        
    }
}
