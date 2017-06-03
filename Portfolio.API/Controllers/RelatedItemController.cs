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
    public class RelatedItemController : Controller
    {
        private readonly IRepository<RelatedItem> _relatedItemRepository;
        private readonly AuthenticationService _authenticationService;

        public RelatedItemController(IRepository<RelatedItem> relatedItemRepository)
        {
            _relatedItemRepository = relatedItemRepository;
            _authenticationService = new AuthenticationService(new UserRepository(relatedItemRepository.DatabaseInfo.Context));
        }
        
        [HttpGet]
        public IEnumerable<RelatedItem> GetAll()
        {
            return _relatedItemRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetRelatedItem")]
        public IActionResult GetById(int id)
        {
            var item = _relatedItemRepository.Find(id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([Required][FromHeader(Name = "Authorization")] string authToken, [FromBody] RelatedItem item)
        {
            if (!_authenticationService.VerifyAuthToken(authToken))
                return BadRequest("Invalid AuthToken");

            if (item == null)
                return BadRequest();

            _relatedItemRepository.AddAndCommit(item);
            return CreatedAtRoute("GetRelatedItem", new { id = item.ID }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [Required][FromHeader(Name = "Authorization")] string authToken, [FromBody] RelatedItem item)
        {
            if (!_authenticationService.VerifyAuthToken(authToken))
                return BadRequest("Invalid AuthToken");

            if (item == null || item.ID != id)
                return BadRequest();

            var repoItem = _relatedItemRepository.Find(id);
            if (repoItem == null)
                return NotFound();

            repoItem.Name = item.Name;

            _relatedItemRepository.UpdateAndCommit(repoItem);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [Required][FromHeader(Name = "Authorization")] string authToken)
        {
            if (!_authenticationService.VerifyAuthToken(authToken))
                return BadRequest("Invalid AuthToken");

            var repoItem = _relatedItemRepository.Find(id);
            if (repoItem == null)
                return NotFound();

            _relatedItemRepository.RemoveAndCommit(id);
            return new NoContentResult();
        }
    }
}