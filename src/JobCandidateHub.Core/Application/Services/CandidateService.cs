using AutoMapper;
using JobCandidateHub.Core.Application.Interfaces;
using JobCandidateHub.Core.Domains.Dtos;
using JobCandidateHub.Core.Domains.Entities;

namespace JobCandidateHub.Core.Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;

        public CandidateService(ICandidateRepository candidateRepository, IMapper mapper)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
        }

        public async Task<CandidateDto> AddOrUpdate(CandidateDto candidate)
        {
            var existCandidate = await _candidateRepository.GetCandidateByEmail(candidate.Email);
            if (existCandidate != null)
            {
                await _candidateRepository.Add(_mapper.Map<Candidate>(candidate));
            } else
            {
                await _candidateRepository.Update(_mapper.Map<Candidate>(candidate));
            }
            return candidate;
        }
    }
}
