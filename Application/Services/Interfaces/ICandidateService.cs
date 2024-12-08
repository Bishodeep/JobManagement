using Application.Model;

namespace Application.Services.Interfaces
{
    public interface ICandidateService
    {
        Task AddOrUpdateAsync(CandidateDto candidateDto);
        Task<List<CandidateDto>> GetAllCandidate();
    }
}
