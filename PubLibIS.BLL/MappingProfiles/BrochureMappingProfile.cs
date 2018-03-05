using AutoMapper;
using PubLibIS.DAL.Models;
using PubLibIS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubLibIS.BLL.MappingProfiles
{
    public class BrochureMappingProfile : Profile
    {
        public BrochureMappingProfile()
        {
            CreateMap<BrochureViewModel, Brochure>();
            CreateMap<Brochure, BrochureViewModel>();
        }
    }
}
