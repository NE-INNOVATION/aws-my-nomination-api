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

        [HttpPost]
        [Route("GetPrograms")]
        [Authorize]
        public List<NominationProgram> GetPrograms(User user)
        {
            return _nominationService.GetPrograms(user);            
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

        [HttpPost]
        [Route("GetProgramsByUserId")]
        [Authorize]
        public List<NominationProgram> GetProgramsByUserId(User user)
        {
            return _nominationService.GetPrograms(user);
        }

        [HttpGet]
        [Route("GetProgramsById")]
        public NominationProgram GetProgramsById([FromQuery] string programId)
        {
           return _nominationService.GetProgramById(programId);
        }

        [HttpGet]
        [Route("GetAllPrograms")]
        [Authorize]
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
        [Authorize]
        public List<ProgramCategory> GetAllProgramsCategories(User user)
        {
            return _nominationService.GetAllProgramCategories(user);
        }

        [HttpGet]
        [Route("GetProgramsForCategories")]
        [Authorize]
        public List<NominationProgram> GetProgramsForCategories([FromQuery] string categoryId)
        {
            return _nominationService.GetProgramsForCategories(categoryId);
        }

        [HttpGet]
        [Route("GetConfigurationForUi")]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
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
