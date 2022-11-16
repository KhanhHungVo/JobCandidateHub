using AutoMapper;
using JobCandidateHub.Core.Application.Interfaces;
using JobCandidateHub.Core.Domains.Dtos;
using JobCandidateHub.Core.Domains.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace JobCandidateHub.Core.Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private const string emailListCacheKey = "emails";

        public CandidateService(ICandidateRepository candidateRepository, IMapper mapper, IMemoryCache memoryCache)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        public async Task<CandidateDto> AddOrUpdate(CandidateDto candidate)
        {

            if (_memoryCache.TryGetValue(emailListCacheKey, out IEnumerable<string> emails))
            {
                // has data from cache
                if (emails.Any(x => x == candidate.Email))
                {
                    await _candidateRepository.Update(_mapper.Map<Candidate>(candidate));
                }
                else
                {
                    await _candidateRepository.Add(_mapper.Map<Candidate>(candidate));
                    var candidates = await _candidateRepository.GetAll();
                    SetListEmailCache(candidates);
                }
            }
            else
            {
                var candidates = await _candidateRepository.GetAll();
                if(candidates.Where(x => x.Email == candidate.Email).Any()){
                    await _candidateRepository.Update(_mapper.Map<Candidate>(candidate));
                } else {
                    await _candidateRepository.Add(_mapper.Map<Candidate>(candidate));
                }
                SetListEmailCache(candidates);
            }
            return candidate;
        }

        private void SetListEmailCache(IEnumerable<Candidate> candidates)
        {
            if (candidates != null && candidates.Any())
            {
                var listEmails = candidates.Select(x => x.Email);
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                                        .SetSlidingExpiration(TimeSpan.FromSeconds(3600))
                                        .SetPriority(CacheItemPriority.Normal)
                                        .SetSize(1024);
                _memoryCache.Set(emailListCacheKey, listEmails, cacheEntryOptions);
            }

        }
    }
}
