using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using my_nomination_api.models;
using my_nomination_api.Services;
using System.Collections.Generic;

namespace my_nomination_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class EligibilityController : ControllerBase
    {
        private readonly EligibilityService _eligibilityService;

        public EligibilityController(EligibilityService eligibilityService)
        {
            _eligibilityService = eligibilityService;
        }

        [HttpPost]
        [Route("GetUserEligibilty")]
        public EligibiltyResponse GetUserEligibilty([FromBody] List<string> userGroups)
        {
            return _eligibilityService.GetUserEligibilty(userGroups);
        }
    }
}
