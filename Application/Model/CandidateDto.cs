using System.ComponentModel.DataAnnotations;

namespace Application.Model
{
    public class CandidateDto
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public string TimeAvailable { get; set; }
        public string LinkedInUrl { get; set; }
        public string GitHubUrl { get; set; }
        [Required]
        public string Comments { get; set; }
    }
}
