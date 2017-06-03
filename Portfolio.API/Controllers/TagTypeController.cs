using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Models;
using Portfolio.API.Repositories;
using Portfolio.API.Services;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Portfolio.API.Controllers
{
    [Route("api/[controller]")]
    public class TagTypeController : Controller
    {
        private readonly IRepository<TagType> _tagTypeRepository;
        private readonly AuthenticationService _authenticationService;

        public TagTypeController(IRepository<TagType> tagTypeRepository)
        {
            _tagTypeRepository = tagTypeRepository;
            _authenticationService = new AuthenticationService(new UserRepository(tagTypeRepository.DatabaseInfo.Context));
        }
        
        [HttpGet]
        public IEnumerable<TagType> GetAll()
        {
            return _tagTypeRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetTagType")]
        public IActionResult GetById(int id)
        {
            var item = _tagTypeRepository.Find(id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([Required][FromHeader(Name = "Authorization")] string authToken, [FromBody] TagType item)
        {
            if (!_authenticationService.VerifyAuthToken(authToken))
                return BadRequest("Invalid AuthToken");

            if (item == null)
                return BadRequest();

            _tagTypeRepository.AddAndCommit(item);
            return CreatedAtRoute("GetTagType", new { id = item.ID }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [Required][FromHeader(Name = "Authorization")] string authToken, [FromBody] TagType item)
        {
            if (!_authenticationService.VerifyAuthToken(authToken))
                return BadRequest("Invalid AuthToken");

            if (item == null || item.ID != id)
                return BadRequest();

            var repoItem = _tagTypeRepository.Find(id);
            if (repoItem == null)
                return NotFound();

            repoItem.Name = item.Name;

            _tagTypeRepository.UpdateAndCommit(repoItem);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [Required][FromHeader(Name = "Authorization")] string authToken)
        {
            if (!_authenticationService.VerifyAuthToken(authToken))
                return BadRequest("Invalid AuthToken");

            var repoItem = _tagTypeRepository.Find(id);
            if (repoItem == null)
                return NotFound();

            _tagTypeRepository.RemoveAndCommit(id);
            return new NoContentResult();
        }
    }
}