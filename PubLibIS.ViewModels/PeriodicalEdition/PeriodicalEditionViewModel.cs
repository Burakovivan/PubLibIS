﻿using PubLibIS.ViewModels.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.ViewModels
{
    public class PeriodicalEditionViewModel
    {

        public int Id { get; set; }
        public int ReleaseNumber { get; set; }
        [CustomDataDisplayFormat]
        public DateTime ReleaseDate { get; set; }
        public int Circulation { get; set; }//тираж

        public int Periodical_Id { get; set; }
    }
}
