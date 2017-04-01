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
        private readonly AuthenticationService _authenticationService;

        public AccountController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
            _authenticationService = new AuthenticationService(userRepository);
        }

        [HttpPost(Name ="CreateUser")]
        public IActionResult Create([FromBody] UserCreate item)
        {
            if (!AuthenticationService.EnableUserCreation)
                return BadRequest("User Creation is currently disabled");

            if (string.IsNullOrWhiteSpace(item.Username) || string.IsNullOrWhiteSpace(item.Email) ||string.IsNullOrWhiteSpace(item.Password))
                return BadRequest("Please provide all Username, Email and Password");

            // Get the User and Authenticate
            User user = _userRepository.GetAllQuery()
                    .Where(x => x.Username.Equals(item.Username))
                    .FirstOrDefault();

            // If the user exists, ABORT!
            if (user != null)
            {
                return BadRequest("This user already exists");
            }

            // Validate the Inputs
            if (!_authenticationService.ValidateUsername(item.Username))
                return BadRequest();
            if (!_authenticationService.ValidateEmail(item.Email))
                return BadRequest();
            if (!_authenticationService.ValidatePassword(item.Password))
                return BadRequest();

            user = new User();
            user.Username = item.Username;
            user.Email = item.Email;

            var userCount = _userRepository.Count + 1;
            user.Password_Hash = _authenticationService.HashPassword(userCount, item.Password);
            user.AuthToken = _authenticationService.GenerateAuthToken(userCount, user.Username);
            _userRepository.Add(user);

            UserAuthenticated authenticatedUser = new UserAuthenticated();
            authenticatedUser.ID = user.ID;
            authenticatedUser.Username = user.Username;
            authenticatedUser.AuthToken = user.AuthToken;
            return new ObjectResult(authenticatedUser);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromHeader(Name = "Authorization")] string authToken)
        {
            if (!_authenticationService.VerifyAuthTokenAndID(id, authToken))
                return BadRequest("Invalid AuthToken");

            var repoItem = _userRepository.Find(id);
            if (repoItem == null)
                return NotFound();

            _userRepository.Remove(id);
            return new NoContentResult();
        }
    }
}
