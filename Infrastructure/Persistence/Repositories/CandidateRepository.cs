using Application.Data.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class CandidateRepository : RepositoryBase<Candidate>, ICandidateRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CandidateRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Candidate> GetByEmailAsync(string email)
        {
            return await _dbContext.Candidates.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
