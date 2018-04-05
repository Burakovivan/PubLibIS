using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace PubLibIS.DAL.Models
{
    public class BaseEntity
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        [Write(false)]
        public DateTime? CreatedDate { get; set; }
        [Write(false)]
        public DateTime? ModifiedDate { get; set; }
    }
}
