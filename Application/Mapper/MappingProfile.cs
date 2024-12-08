using Application.Model;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CandidateDto, Candidate>().ReverseMap();
        }
    }
}
