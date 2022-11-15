using Microsoft.AspNetCore.Mvc;

namespace JobCandidateHub.Api.Controllers
{
    [ApiController]
    [Route("api/candidates")]
    public class CandidateController : ControllerBase
    {
        [HttpPost]
        public Task<IActionResult> Add()
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public Task<IActionResult> Update()
        {
            throw new NotImplementedException();
        }
    }
}
