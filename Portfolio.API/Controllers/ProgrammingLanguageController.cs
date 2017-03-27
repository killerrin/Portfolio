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
    public class ProgrammingLanguageController : Controller
    {
        private readonly IRepository<ProgrammingLanguage> _programmingLanguageRepository;
        public ProgrammingLanguageController(IRepository<ProgrammingLanguage> pLRepository)
        {
            _programmingLanguageRepository = pLRepository;
        }
        
        [HttpGet]
        public IEnumerable<ProgrammingLanguage> GetAll()
        {
            return _programmingLanguageRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetProgrammingLanguage")]
        public IActionResult GetById(int id)
        {
            var item = _programmingLanguageRepository.Find(id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProgrammingLanguage item)
        {
            if (item == null)
                return BadRequest();

            _programmingLanguageRepository.Add(item);
            return CreatedAtRoute("GetProgrammingLanguage", new { id = item.Key }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProgrammingLanguage item)
        {
            if (item == null || item.Key != id)
                return BadRequest();

            var programmingLanguage = _programmingLanguageRepository.Find(id);
            if (programmingLanguage == null)
                return NotFound();

            programmingLanguage.Name = item.Name;

            _programmingLanguageRepository.Update(programmingLanguage);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var programmingLanguage = _programmingLanguageRepository.Find(id);
            if (programmingLanguage == null)
                return NotFound();

            _programmingLanguageRepository.Remove(id);
            return new NoContentResult();
        }
    }
}