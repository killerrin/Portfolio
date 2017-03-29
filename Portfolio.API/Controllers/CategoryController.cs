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
    public class CategoryController : Controller
    {
        private readonly IRepository<Category> _categoryRepository;
        public CategoryController(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        
        [HttpGet]
        public IEnumerable<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetCategory")]
        public IActionResult GetById(int id)
        {
            var item = _categoryRepository.Find(id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Category item)
        {
            if (item == null)
                return BadRequest();

            _categoryRepository.Add(item);
            return CreatedAtRoute("GetCategory", new { id = item.ID }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Category item)
        {
            if (item == null || item.ID != id)
                return BadRequest();

            var repoItem = _categoryRepository.Find(id);
            if (repoItem == null)
                return NotFound();

            repoItem.Name = item.Name;

            _categoryRepository.Update(repoItem);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var repoItem = _categoryRepository.Find(id);
            if (repoItem == null)
                return NotFound();

            _categoryRepository.Remove(id);
            return new NoContentResult();
        }
    }
}