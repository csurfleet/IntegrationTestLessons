using System;
using Lesson01.UsersApi.Models;
using Lesson01.UsersApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Lesson01.UsersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        // GET api/users/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id) => _userRepository.Get(id);
    }
}
