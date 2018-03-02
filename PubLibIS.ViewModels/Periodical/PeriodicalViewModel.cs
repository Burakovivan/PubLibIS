using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PubLibIS.ViewModels
{
    public class PeriodicalViewModel
    {
        public int Id { get; set; }
        public string ISSN { get; set; }
        public PeriodicalType Type { get; set; }
        public string Name { get; set; }
        public DateTime Foundation { get; set; }
        public bool IsPublished { get; set; }

        public SelectList PublishingHouseSelectList { get; set; }
        public PublishingHouseViewModel PublishingHouse { get; set; }
        public IEnumerable<PeriodicalEditionViewModel> PeriodicalEditions { get; set; }
    }
}
