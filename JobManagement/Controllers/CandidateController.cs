using Application.Exceptions;
using Application.Model;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var candidate = await _candidateService.GetAllCandidate();
            return Ok(candidate);
        }
        [HttpPost]
        public async Task<IActionResult> AddCandidate(CandidateDto candidateDto)
        {
            if (IsValidCandidate(candidateDto))
            {
                await _candidateService.AddOrUpdateAsync(candidateDto);
                return Ok();
            }
            return BadRequest();
        }

        private bool IsValidCandidate(CandidateDto candidateDto)
        {
            if (candidateDto == null)
            {
                throw new BadRequestsException("Candidate cannot be null");
            }
            if (string.IsNullOrEmpty(candidateDto.Email) || !IsValidEmail(candidateDto.Email))
            {
                throw new BadRequestsException("Invalid email address.");
            }
            if ( string.IsNullOrEmpty(candidateDto.FirstName)
                || string.IsNullOrEmpty(candidateDto.LastName) || string.IsNullOrEmpty(candidateDto.Comments))
            {
                throw new BadRequestsException("Required values are not filled.");
            }
            return true;
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
