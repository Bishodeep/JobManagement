using Application.Data.Repository;
using Application.Model;
using Application.Services.Implementations;
using AutoMapper;
using Domain.Entities;
using Moq;

namespace Application.UnitTests.Services
{
    public class CandidateServiceTests
    {
        private readonly Mock<ICandidateRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CandidateService _service;

        public CandidateServiceTests()
        {
            _mockRepository = new Mock<ICandidateRepository>();
            _mockMapper = new Mock<IMapper>();
            _service = new CandidateService(_mockRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task AddOrUpdateAsync_GivenExistingCandidate_ShouldUpdatesCandidate()
        {
            var candidateDto = new CandidateDto
            {
                Email = "existing@example.com",
                FirstName = "test",
                LastName = "test",
                PhoneNumber = "123456789",
                Comments = "Updated Comments"
            };

            var existingCandidate = new Candidate
            {
                Email = "existing@example.com",
                FirstName = "OldName",
                LastName = "test",
                PhoneNumber = "987654321",
                Comments = "Old Comments",
                ModifiedDate = DateTime.MinValue
            };

            _mockRepository.Setup(r => r.GetByEmailAsync(candidateDto.Email)).ReturnsAsync(existingCandidate);

            await _service.AddOrUpdateAsync(candidateDto);

            _mockRepository.Verify(r => r.Update(It.Is<Candidate>(c =>
                c.Email == candidateDto.Email &&
                c.FirstName == candidateDto.FirstName &&
                c.LastName == candidateDto.LastName &&
                c.PhoneNumber == candidateDto.PhoneNumber &&
                c.Comments == candidateDto.Comments &&
                c.ModifiedDate > DateTime.MinValue
            )), Times.Once);
        }

        [Fact]
        public async Task AddOrUpdateAsync_GivenNewCandidate_ShouldAddsCandidate()
        {
            var candidateDto = new CandidateDto
            {
                Email = "new@example.com",
                FirstName = "tester",
                LastName = "tester",
                PhoneNumber = "123456789",
                Comments = "New Candidate"
            };

            var candidateEntity = new Candidate
            {
                Email = candidateDto.Email,
                FirstName = candidateDto.FirstName,
                LastName = candidateDto.LastName,
                PhoneNumber = candidateDto.PhoneNumber,
                Comments = candidateDto.Comments
            };

            _mockRepository.Setup(r => r.GetByEmailAsync(candidateDto.Email)).ReturnsAsync((Candidate)null);
            _mockMapper.Setup(m => m.Map<Candidate>(candidateDto)).Returns(candidateEntity);

            await _service.AddOrUpdateAsync(candidateDto);

            _mockRepository.Verify(r => r.AddAsync(candidateEntity), Times.Once);
        }

        [Fact]
        public async Task GetAllCandidate_ShouldReturnsMappedCandidates()
        {
            var candidates = new List<Candidate>
        {
            new Candidate { Email = "test1@example.com", FirstName = "test1", LastName = "test" },
            new Candidate { Email = "test2@example.com", FirstName = "test2", LastName = "test" }
        };

            var candidateDtos = new List<CandidateDto>
        {
            new CandidateDto { Email = "test1@example.com", FirstName = "test1", LastName = "test" },
            new CandidateDto { Email = "test2@example.com", FirstName = "test2", LastName = "test" }
        };

            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(candidates);
            _mockMapper.Setup(m => m.Map<List<CandidateDto>>(candidates)).Returns(candidateDtos);

            var result = await _service.GetAllCandidate();

            Assert.Equal(candidateDtos, result);
        }
    }
}
