using PubLibIS.Domain.Entities;
using PubLibIS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.DAL.ResponseModels
{
    public class GetPeriodicalResponseModel
    {
        public int Id{ get; set; }
        public string ISSN { get; set; }
        public string Name { get; set; }
        public DateTime Foundation { get; set; }
        public PeriodicalType Type { get; set; } 
        public bool IsPublished { get; set; }
        public int? PublishingHouse_Id  { get; set; }
        public PublishingHouse PublishingHouse { get; set; }
        public IEnumerable<PeriodicalEdition > PeriodicalEditions{ get; set; }
    }
}
