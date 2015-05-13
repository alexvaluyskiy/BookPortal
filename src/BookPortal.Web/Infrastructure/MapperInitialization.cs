using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookPortal.Web.Domain.Models;
using BookPortal.Web.Models;

namespace BookPortal.Web.Infrastructure
{
    public class MapperInitialization
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                int FirstContestYear = 0, LastContestYear = 0;
                cfg.CreateMap<Award, AwardResponse>()
                   // .ForMember(d => d.FirstContestYear, opt => opt.MapFrom(src => FirstContestYear))
                    //.ForMember(d => d.LastContestYear, opt => opt.MapFrom(src => LastContestYear))
                    //.ForMember(d => d.CountryName, opt => opt.MapFrom(src => src.Country.Name))
                    //.ForMember(d => d.LanguageName, opt => opt.MapFrom(src => src.Language.Name))
                    ;
            });
        }
    }
}
