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

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetById(int id, [Required][FromHeader(Name = "Authorization")] string authToken)
        {
            // Verify the Authorization Token
            if (!_authenticationService.VerifyAuthTokenAndID(id, authToken))
                return BadRequest("Invalid AuthToken");

            // Get the user
            var item = _userRepository.Find(id);
            if (item == null)
                return NotFound();

            // Create the safe copy and return
            UserGet user = new UserGet();
            user.ID = item.ID;
            user.Username = item.Username;
            user.Email = item.Email;
            return new ObjectResult(user);
        }

        [HttpPost(Name = "CreateUser")]
        public IActionResult Create([FromBody] UserCreate item)
        {
            if (!AuthenticationService.EnableUserCreation)
                return BadRequest("User Creation is currently disabled by the administrator ");

            if (string.IsNullOrWhiteSpace(item.Username) || string.IsNullOrWhiteSpace(item.Email) || string.IsNullOrWhiteSpace(item.Password))
                return BadRequest("Please provide Username and Current Password");

            // Get the User and Authenticate
            User user = _userRepository.GetAllQuery()
                    .Where(x => x.Username.ToLower().Equals(item.Username.ToLower()))
                    .FirstOrDefault();

            // If the user exists, ABORT!
            if (user != null)
                return BadRequest("This user already exists");

            // Validate the Inputs
            if (!_authenticationService.ValidateUsername(item.Username))
                return BadRequest("The username is invalid, has already been taken or does not meet the minimum requirements");
            if (!_authenticationService.ValidateEmail(item.Email))
                return BadRequest("The email is invalid or does not meet the minimum requirements");
            if (!_authenticationService.ValidatePassword(item.Password))
                return BadRequest("The password is invalid or does not meet the minimum requirements");

            user = new User();
            user.Username = item.Username;
            user.Email = item.Email;

            var userCount = _userRepository.Count + 1;
            user.Password_Hash = _authenticationService.HashPassword(item.Password);
            user.AuthToken = _authenticationService.GenerateAuthToken(userCount, item.Username);
            _userRepository.AddAndCommit(user);

            UserAuthenticated authenticatedUser = new UserAuthenticated();
            authenticatedUser.ID = user.ID;
            authenticatedUser.Username = user.Username;
            authenticatedUser.AuthToken = user.AuthToken;
            return new ObjectResult(authenticatedUser);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [Required][FromHeader(Name = "Authorization")] string authToken, [FromBody] UserEdit item)
        {
            if (item == null)
                return BadRequest();
            if (string.IsNullOrWhiteSpace(item.CurrentPassword))
                return BadRequest("Please provide Username and Current Password");

            // Verify the Authorization Token
            if (!_authenticationService.VerifyAuthTokenAndID(id, authToken))
                return BadRequest("Invalid AuthToken");

            // Get the current user
            var repoItem = _userRepository.Find(id);
            if (repoItem == null)
                return NotFound();

            // Make sure the current password is correctly set as a third layer of security
            if (_authenticationService.VerifyPassword(item.CurrentPassword, repoItem.Password_Hash))
            {
                // If the new username doesn't equal the current username...
                if (!string.IsNullOrWhiteSpace(item.NewUsername))
                {
                    if (!_authenticationService.ValidateUsername(item.NewUsername))
                        return BadRequest("The new username is invalid, has already been taken or does not meet the minimum requirements");

                    // Search that new username to determine if it is available
                    User user = _userRepository.GetAllQuery()
                        .Where(x => x.Username.ToLower().Equals(item.NewUsername.ToLower()))
                        .FirstOrDefault();

                    // If it isn't, notify the user it is unavailable
                    if (user != null)
                        return BadRequest("This user already exists");

                    // Otherwise, set the new username
                    repoItem.Username = item.NewUsername;
                }

                // If we have a new password, go through the steps to generate a new password and auth token
                if (!string.IsNullOrWhiteSpace(item.NewPassword))
                {
                    if (!_authenticationService.ValidatePassword(item.NewPassword))
                        return BadRequest("The new password contains invalid characters or does not meet the minimum requirements");

                    repoItem.Password_Hash = _authenticationService.HashPassword(item.NewPassword);
                    repoItem.AuthToken = _authenticationService.GenerateAuthToken(repoItem.ID, repoItem.Username);
                }

                // Determine if we need to change the email
                if (!string.IsNullOrWhiteSpace(item.NewEmail))
                {
                    if (_authenticationService.ValidateEmail(item.NewEmail))
                        repoItem.Email = item.NewEmail;
                    else
                        return BadRequest("The email contains invalid characters or does not meet the minimum requirements");
                }

                // Update the user
                _userRepository.UpdateAndCommit(repoItem);

                // Return the new authenticated user
                UserAuthenticated authenticatedUser = new UserAuthenticated();
                authenticatedUser.ID = repoItem.ID;
                authenticatedUser.Username = repoItem.Username;
                authenticatedUser.AuthToken = repoItem.AuthToken;
                return new ObjectResult(authenticatedUser);
            }

            return BadRequest("Current Password is incorrect");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [Required][FromHeader(Name = "Authorization")] string authToken)
        {
            if (!_authenticationService.VerifyAuthTokenAndID(id, authToken))
                return BadRequest("Invalid AuthToken");

            var repoItem = _userRepository.Find(id);
            if (repoItem == null)
                return NotFound();

            _userRepository.RemoveAndCommit(id);
            return new NoContentResult();
        }
    }
}
