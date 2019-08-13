using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase {
        private readonly IUsersRepository _userRepo;
        public UsersController (IUsersRepository userRepo) {
            _userRepo = userRepo;
        }

        [HttpGet]
        [Route ("{id}")]
        public async Task<ActionResult<User>> GetByID (int id) {
            var users = await _userRepo.GetUserDetails (id);
            if (users==null)
            {
                return NotFound();
            }
            return users;
        }

        [HttpGet]
        [Route ("all")]
        public async Task<ActionResult<List<User>>> Get()  {
            return await _userRepo.GetUsersList();
        }
    }
}