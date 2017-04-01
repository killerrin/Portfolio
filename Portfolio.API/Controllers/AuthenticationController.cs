using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Repositories;
using Portfolio.API.Models;
using Portfolio.API.Services;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Portfolio.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IRepository<User> _userRepository;
        public AuthenticationController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpPost(Name = "Login")]
        public IActionResult Login([FromBody] UserLogin item)
        {
            if (string.IsNullOrWhiteSpace(item.Username) || string.IsNullOrWhiteSpace(item.Password))
                return BadRequest();

            AuthenticationService service = new AuthenticationService(_userRepository);

            // Get the User and Authenticate
            User user = _userRepository.GetAll()
                    .Where(x => x.Username.Equals(item.Username))
                    .FirstOrDefault();

            if (user != null)
            {
                if (service.VerifyPassword(item.Password, user.Password_Hash))
                {
                    UserAuthenticated authenticatedUser = new UserAuthenticated();
                    authenticatedUser.ID = user.ID;
                    authenticatedUser.Username = user.Username;
                    authenticatedUser.AuthToken = user.AuthToken;
                    return new ObjectResult(authenticatedUser);
                }
            }

            // Run the hashing algorithm a couple times to make it impossible to determine partial success/failure based off of Server Response Times
            for (int i = 0; i < 3; i++)
            {
                service.FakeHash();
            }
            return BadRequest("Either your username or password is invalid. Please try again.");
        }
    }
}
