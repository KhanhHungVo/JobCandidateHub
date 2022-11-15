using JobCandidateHub.Core.Domains.Dtos;

namespace JobCandidateHub.Core.Application.Interfaces
{
    public interface ICandidateService
    {
        Task<int> AddOrUpdate(CandidateDto candidate);
    }
}
