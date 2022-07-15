using AutoMapper;
using SampleWebAPI.Domain;
using SampleWebAPI.DTO;

namespace SampleWebAPI.Profiles
{
    public class SamuraiProfile : Profile
    {
        public SamuraiProfile()
        {
            CreateMap<Samurai, SamuraiReadDTO>();
            CreateMap<SamuraiReadDTO, Samurai>();
        }
    }
}
