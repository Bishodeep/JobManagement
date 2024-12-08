using Application.Exceptions;
using Application.Model;
using Application.Services.Interfaces;
using JobManagement.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Application.UnitTests.Controllers
{
    public class CandidateControllerTests
    {
        private readonly Mock<ICandidateService> _mockCandidateService;
        private readonly CandidateController _controller;

        public CandidateControllerTests()
        {
            _mockCandidateService = new Mock<ICandidateService>();
            _controller = new CandidateController(_mockCandidateService.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnOkResultWithCandidates()
        {
            var candidates = new List<CandidateDto>
        {
            new CandidateDto { Email = "test1@example.com", FirstName = "test", LastName = "test", Comments = "Good candidate" },
            new CandidateDto { Email = "test2@example.com", FirstName = "test2", LastName = "test2", Comments = "Excellent candidate" }
        };
            _mockCandidateService.Setup(s => s.GetAllCandidate()).ReturnsAsync(candidates);

            var result = await _controller.Get();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(candidates, okResult.Value);
        }

        [Fact]
        public async Task AddCandidate_GivenValidCandidate_ShouldReturnOk()
        {
            var candidateDto = new CandidateDto
            {
                Email = "test3@example.com",
                FirstName = "test3",
                LastName = "test3",
                Comments = "Good candidate"
            };

            _mockCandidateService.Setup(s => s.AddOrUpdateAsync(candidateDto)).Returns(Task.CompletedTask);

            var result = await _controller.AddCandidate(candidateDto);

            Assert.IsType<OkResult>(result);
            _mockCandidateService.Verify(s => s.AddOrUpdateAsync(candidateDto), Times.Once);
        }

        [Fact]
        public async Task AddCandidate_GivenInvalidCandidate_ShouldReturnBadRequest()
        {
            var candidateDto = new CandidateDto(); // Missing required fields

            await Assert.ThrowsAsync<BadRequestsException>(() => _controller.AddCandidate(candidateDto));
        }

        [Fact]
        public async Task AddCandidate_GivenNullCandidate_ShouldThrowException()
        {
            CandidateDto candidateDto = null;

            await Assert.ThrowsAsync<BadRequestsException>(() => _controller.AddCandidate(candidateDto));
        }
    }
}
