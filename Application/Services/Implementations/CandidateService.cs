using Application.Data.Repository;
using Application.Model;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Entities;
using System.ComponentModel;

namespace Application.Services.Implementations
{
    public class CandidateService:ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;

        public CandidateService(ICandidateRepository candidateRepository,IMapper mapper)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
        }
        public async Task AddOrUpdateAsync(CandidateDto candidateDto)
        {
            var existingCandidate = await _candidateRepository.GetByEmailAsync(candidateDto.Email);
            if(existingCandidate != null)
            {
                existingCandidate.Email = candidateDto.Email;
                existingCandidate.FirstName = candidateDto.FirstName;   
                existingCandidate.LastName = candidateDto.LastName;
                existingCandidate.PhoneNumber = candidateDto.PhoneNumber;
                existingCandidate.Comments = candidateDto.Comments;
                existingCandidate.ModifiedDate = DateTime.Now;

                await _candidateRepository.Update(existingCandidate);
            }
            else
            {
               await _candidateRepository.AddAsync(_mapper.Map<Candidate>(candidateDto));
            }
        }
        public async Task<List<CandidateDto>> GetAllCandidate()
        {
            return _mapper.Map<List<CandidateDto>>( await _candidateRepository.GetAllAsync());
        }
    }
}
