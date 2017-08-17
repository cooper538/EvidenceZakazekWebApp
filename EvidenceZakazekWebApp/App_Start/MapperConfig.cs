using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EvidenceZakazekWebApp.App_Start
{
    // from https://stackoverflow.com/a/35522888/6355668
    public class MapperConfig
    {
        public static MapperConfiguration MapperConfiguration()
        {
            var config = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            config.AssertConfigurationIsValid();

            return config;
        }
    }
}