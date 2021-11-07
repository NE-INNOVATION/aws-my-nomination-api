using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using my_nomination_api.models;
using my_nomination_api.Services;
using System;
using System.Collections.Generic;

namespace my_nomination_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public List<UserRegion> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        [HttpGet]
        [Route("GetUserByEnterpriseId")]
        public UserRegion GetUserByEnterpriseId(string enterpriseId)
        {
            return _userService.GetUserByEnterpriseId(enterpriseId);
        }

        [HttpGet]
        [Route("GetUsersByRegion")]
        public List<UserRegion> GetUsersByRegion(string regionId)
        {
            return _userService.GetUsersByRegion(regionId);
        }
              

        [HttpPost]
        [Route("CreateUser")]
        public ActionResult<UserRegion> CreateUser([FromBody] UserRegion userRegion)
        {
            var userExists = _userService.GetUserByEnterpriseId(userRegion.EnterpriseId);
            if (userExists != null)
            {
                return BadRequest();
            }

            return _userService.CreateUser(userRegion);
        }

        [HttpPost]
        [Route("UpdateUser")]
        public ActionResult<UserRegion> UpdateUser([FromBody] UserRegion userRegion)
        {
            var userExists = _userService.GetUserByEnterpriseId(userRegion.EnterpriseId);
            if (userExists == null)
            {
                return BadRequest();
            }

            return _userService.UpdateUser(userRegion);
        }

        [HttpPost]
        [Route("GetUserRegionsByGroups")]
        public List<Region> GetUserRegionsByGroups([FromBody] List<string> userGroups)
        {
            return _userService.GetUserRegionsByGroups(userGroups);
        }
    }
}
