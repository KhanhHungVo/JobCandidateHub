using System.ComponentModel.DataAnnotations;

namespace JobCandidateHub.Core.Domains.Dtos
{
    public class CandidateDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNUmber { get; set; }
        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please input valid email")]
        public string Email { get; set; }
        public string TimeInterval { get; set; }
        public string LinkedinUrl { get; set; }
        public string GithubUrl { get; set; }
        [Required]
        public string Comment { get; set; }


    }
}
