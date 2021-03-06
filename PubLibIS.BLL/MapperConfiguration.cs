﻿using AutoMapper;
using PubLibIS.BLL.MappingProfiles;

namespace PubLibIS.BLL
{
    public static class MappingProfile
    {
        public static MapperConfiguration InitializeAutoMapper()
        {
            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PeriodicalMappingProfile());
                cfg.AddProfile(new AuthorMappingProfile());  
                cfg.AddProfile(new BookMappingProfile());  
                cfg.AddProfile(new BrochureMappingProfile());
                cfg.AddProfile(new PublishingHouseMappingProfile());
            });

            return config;
        }
    }
}
