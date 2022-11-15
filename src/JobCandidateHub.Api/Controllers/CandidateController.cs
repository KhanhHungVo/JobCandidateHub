using JobCandidateHub.Core.Application.Interfaces;
using JobCandidateHub.Core.Domains.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace JobCandidateHub.Api.Controllers
{
    [ApiController]
    [Route("api/candidates")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CandidateDto candidate)
        {
            return Ok(await _candidateService.AddOrUpdate(candidate));
        }
    }
}
