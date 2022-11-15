using AutoMapper;
using JobCandidateHub.Core.Domains.Dtos;
using JobCandidateHub.Core.Domains.Entities;

namespace JobCandidateHub.Core.Application.AutoMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CandidateDto, Candidate>().ReverseMap();
        }

    }
}
