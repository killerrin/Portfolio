using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Models;
using Portfolio.API.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Portfolio.API.Controllers
{
    [Route("api/[controller]")]
    public class KeywordController : Controller
    {
        private readonly IRepository<Keyword> _keywordRepository;
        public KeywordController(IRepository<Keyword> keywordRepository)
        {
            _keywordRepository = keywordRepository;
        }
        
        [HttpGet]
        public IEnumerable<Keyword> GetAll()
        {
            return _keywordRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetKeyword")]
        public IActionResult GetById(int id)
        {
            var item = _keywordRepository.Find(id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Keyword item)
        {
            if (item == null)
                return BadRequest();

            _keywordRepository.Add(item);
            return CreatedAtRoute("GetKeyword", new { id = item.ID }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Keyword item)
        {
            if (item == null || item.ID != id)
                return BadRequest();

            var repoItem = _keywordRepository.Find(id);
            if (repoItem == null)
                return NotFound();

            repoItem.Name = item.Name;

            _keywordRepository.Update(repoItem);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var repoItem = _keywordRepository.Find(id);
            if (repoItem == null)
                return NotFound();

            _keywordRepository.Remove(id);
            return new NoContentResult();
        }
    }
}