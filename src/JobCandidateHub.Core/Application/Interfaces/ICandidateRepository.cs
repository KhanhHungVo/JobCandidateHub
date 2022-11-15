using JobCandidateHub.Core.Domains.Entities;

namespace JobCandidateHub.Core.Application.Interfaces
{
    public interface ICandidateRepository
    {
        Task Add(Candidate candidate);
        Task Update(Candidate candidate);
        Task<Candidate?> GetCandidateByEmail(string email);
    }
}
