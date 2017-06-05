using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Models;
using Portfolio.API.Repositories;
using Portfolio.API.Services;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Portfolio.API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    public class TagController : Controller
    {
        private readonly IRepository<Tag> _tagRepository;
        private readonly AuthenticationService _authenticationService;

        public TagController(IRepository<Tag> tagRepository)
        {
            _tagRepository = tagRepository;
            _authenticationService = new AuthenticationService(new UserRepository(tagRepository.DatabaseInfo.Context));
        }
        
        [HttpGet]
        public IEnumerable<Tag> GetAll()
        {
            return _tagRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetTag")]
        public IActionResult GetById(int id)
        {
            var item = _tagRepository.Find(id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([Required][FromHeader(Name = "Authorization")] string authToken, [FromBody] Tag item)
        {
            if (!_authenticationService.VerifyAuthToken(authToken))
                return BadRequest("Invalid AuthToken");

            if (item == null)
                return BadRequest();

            _tagRepository.AddAndCommit(item);
            return CreatedAtRoute("GetTag", new { id = item.ID }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [Required][FromHeader(Name = "Authorization")] string authToken, [FromBody] Tag item)
        {
            if (!_authenticationService.VerifyAuthToken(authToken))
                return BadRequest("Invalid AuthToken");

            if (item == null || item.ID != id)
                return BadRequest();

            var repoItem = _tagRepository.Find(id);
            if (repoItem == null)
                return NotFound();

            repoItem.Name = item.Name;
            repoItem.TagTypeID = item.TagTypeID;

            _tagRepository.UpdateAndCommit(repoItem);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [Required][FromHeader(Name = "Authorization")] string authToken)
        {
            if (!_authenticationService.VerifyAuthToken(authToken))
                return BadRequest("Invalid AuthToken");

            var repoItem = _tagRepository.Find(id);
            if (repoItem == null)
                return NotFound();

            _tagRepository.RemoveAndCommit(id);
            return new NoContentResult();
        }
    }
}