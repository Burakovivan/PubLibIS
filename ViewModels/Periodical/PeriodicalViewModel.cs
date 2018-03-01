using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class PeriodicalViewModel
    {
        public int Id { get; set; }
        public string ISSN { get; set; }
        public PeriodicalType Type { get; set; }
        public string Name { get; set; }
        public DateTime Foundation { get; set; }
        public bool IsPublished { get; set; }

        public virtual PublishingHouseViewModel PublishingHouse { get; set; }
        public virtual ICollection<PeriodicalEditionViewModel> PeriodicalEditions { get; set; }
    }
}
