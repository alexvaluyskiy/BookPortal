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
                cfg.CreateMap<Award, AwardResponse>();
            });
        }
    }
}
