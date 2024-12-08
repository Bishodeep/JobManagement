using Domain.Entities;

namespace Application.Data.Repository
{
    public interface ICandidateRepository:IRepository<Candidate>
    {
        Task<Candidate> GetByEmailAsync(string email);
    }
}
