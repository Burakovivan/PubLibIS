using AutoMapper;
using PubLibIS.Models.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AuthorDLL = PubLibIS_BLL.Model.Author;

namespace PubLibIS
{
    public class MapperConfig
    {
        public static void Initialize()
        {
            //Mapper.Initialize(cfg =>
            //{
            //    cfg.CreateMap<AuthorDLL, AuthorViewModel>();
            //});
        }
    }
}