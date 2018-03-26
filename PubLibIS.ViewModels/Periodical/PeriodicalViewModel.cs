using PubLibIS.ViewModels.Attributes;
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
        public PeriodicalViewModel() { }
        public int Id { get; set; }
        public string ISSN { get; set; }
        public string Name { get; set; }
        public PeriodicalTypeViewModel Type { get; set; }
        [CustomDataDisplayFormat]
        public DateTime Foundation { get; set; }
        public bool IsPublished { get; set; }
        public int PublicationsCount => PeriodicalEditions == null ? 0 : PeriodicalEditions.Count();
        public int PublishingHouse_Id { get; set; }
        public PublishingHouseViewModel PublishingHouse { get; set; }
        public IEnumerable<PeriodicalEditionViewModel> PeriodicalEditions { get; set; }

        public SelectList PublishingHouseSelectList { get; set; }
        public SelectList PeriodicalTypeSelectList { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
