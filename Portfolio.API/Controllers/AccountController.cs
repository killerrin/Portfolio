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
    public class AccountController : Controller
    {
        private readonly IRepository<User> _userRepository;
        public AccountController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost(Name ="CreateUser")]
        public IActionResult Create([FromBody] UserCreate item)
        {
            if (!AuthenticationService.EnableUserCreation)
                return BadRequest("User Creation is currently disabled");

            if (string.IsNullOrWhiteSpace(item.Username) || string.IsNullOrWhiteSpace(item.Email) ||string.IsNullOrWhiteSpace(item.Password))
                return BadRequest("Please provide all Username, Email and Password");

            AuthenticationService service = new AuthenticationService(_userRepository);

            // Get the User and Authenticate
            User user = _userRepository.GetAll()
                    .Where(x => x.Username.Equals(item.Username))
                    .FirstOrDefault();

            // If the user exists, ABORT!
            if (user != null)
            {
                // Run the hashing algorithm a couple times to make it impossible to determine partial success/failure based off of Server Response Times
                for (int i = 0; i < 3; i++)
                {
                    service.FakeHash();
                }
                return BadRequest("This user already exists");
            }

            // Validate the Inputs
            if (!service.ValidateUsername(item.Username))
                return BadRequest();
            if (!service.ValidateEmail(item.Email))
                return BadRequest();
            if (!service.ValidatePassword(item.Password))
                return BadRequest();

            user = new User();
            user.Username = item.Username;
            user.Email = item.Email;

            var userCount = _userRepository.Count + 1;
            user.Password_Hash = service.HashPassword(userCount, item.Password);
            user.AuthToken = service.GenerateAuthToken(userCount, user.Username);
            _userRepository.Add(user);

            UserAuthenticated authenticatedUser = new UserAuthenticated();
            authenticatedUser.ID = user.ID;
            authenticatedUser.Username = user.Username;
            authenticatedUser.AuthToken = user.AuthToken;
            return new ObjectResult(authenticatedUser);
        }
    }
}
