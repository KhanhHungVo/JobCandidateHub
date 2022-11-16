namespace JobCandidateHub.Core.Domains.Entities
{
    public class Candidate
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string TimeInterval { get; set; }
        public string LinkedinUrl { get; set; }
        public string GithubUrl { get; set; }
        public string Comment { get; set; }
    }
}
