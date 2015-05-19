using AutoMapper;
using BookPortal.Reviews.Domain.Models;
using BookPortal.Reviews.Model;

namespace BookPortal.Reviews.Infrastructure
{
    public class MapperInitialization
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Review, ReviewResponse>();
            });
        }
    }
}
