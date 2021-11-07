using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using my_nomination_api.models;
using my_nomination_api.Services;

namespace my_nomination_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class ProgrammController : Controller
    {
        private readonly NominationService _nominationService;

        public ProgrammController(NominationService nominationService)
        {
            _nominationService = nominationService;
        }
               
        [HttpGet]
        [Route("GetActivePrograms")]
        public List<NominationProgram> GetActivePrograms()
        {
            return _nominationService.GetAllActiveProgram();
        }

        [HttpGet]
        [Route("GetCompletedPrograms")]
        public List<NominationProgram> GetCompletedPrograms()
        {
            return _nominationService.GetCompletedPrograms();
        }

        [HttpGet]
        [Route("GetProgramsByUserRegion")]
        public List<NominationProgram> GetProgramsByUserRegion(string regionId)
        {
            return _nominationService.GetProgramsByRegionId(regionId);
        }

        [HttpGet]
        [Route("GetProgramsById")]
        public NominationProgram GetProgramsById([FromQuery] string programId)
        {
           return _nominationService.GetProgramById(programId);
        }

        [HttpGet]
        [Route("GetAllPrograms")]
        public List<NominationProgram> GetAllPrograms()
        {
            return _nominationService.GetAllProgram();
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public List<User> GetAllUsers()
        {
            return _nominationService.GetAllUsers();
        }

        [HttpPost]
        [Route("GetAllProgramsCategories")]
        public List<Category> GetAllProgramsCategories([FromBody] List<string> userGroups)
        {
            return _nominationService.GetAllProgramCategories(userGroups);
        }

        [HttpGet]
        [Route("GetProgramsForCategories")]
        public List<NominationProgram> GetProgramsForCategories([FromQuery] string categoryId)
        {
            return _nominationService.GetProgramsForCategories(categoryId);
        }

        [HttpGet]
        [Route("GetConfigurationForUi")]
         public List<NominationProgram> GetConfigurationForUi()
        {
            return _nominationService.GetAllProgram();
        }

        public User GetUser(string userId)
        {
            return _nominationService.GetUser(userId);
        }
             
        [HttpPost]
        [Route("CreateProgram")]
        public NominationProgram CreateProgram(NominationProgram nominationProgram)
        {
            var program = _nominationService.GetProgramById(nominationProgram.ProgramId);
            if (string.IsNullOrEmpty(nominationProgram.ProgramId) && program == null)
            {
                return _nominationService.CreateNominationProgram(nominationProgram);
            }

            nominationProgram.Id = program.Id;
            return _nominationService.UpdateNominationProgram(nominationProgram);

        }


        [HttpPost]
        [Route("UpdateProgram")]
         public ActionResult<NominationProgram> UpdateProgram(NominationProgram nominationProgram)
        {
            var program = _nominationService.GetProgramById(nominationProgram.ProgramId);
            if (string.IsNullOrEmpty(nominationProgram.ProgramId) && program == null)
            {
                return BadRequest();
            }

            nominationProgram.Id = program.Id;
            return _nominationService.UpdateNominationProgram(nominationProgram);

        }

    }
}
