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
    public class FrameworkController : Controller
    {
        private readonly IRepository<Framework> _frameworkRepository;
        public FrameworkController(IRepository<Framework> frameworkRepository)
        {
            _frameworkRepository = frameworkRepository;
        }
        
        [HttpGet]
        public IEnumerable<Framework> GetAll()
        {
            return _frameworkRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetFramework")]
        public IActionResult GetById(int id)
        {
            var item = _frameworkRepository.Find(id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Framework item)
        {
            if (item == null)
                return BadRequest();

            _frameworkRepository.Add(item);
            return CreatedAtRoute("GetFramework", new { id = item.Key }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Framework item)
        {
            if (item == null || item.Key != id)
                return BadRequest();

            var repoItem = _frameworkRepository.Find(id);
            if (repoItem == null)
                return NotFound();

            repoItem.Name = item.Name;

            _frameworkRepository.Update(repoItem);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var repoItem = _frameworkRepository.Find(id);
            if (repoItem == null)
                return NotFound();

            _frameworkRepository.Remove(id);
            return new NoContentResult();
        }
    }
}