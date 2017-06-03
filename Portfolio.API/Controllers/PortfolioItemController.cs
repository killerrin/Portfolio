using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio.API.Models;
using Portfolio.API.Repositories;
using Portfolio.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Portfolio.API.Controllers
{
    [Route("api/[controller]")]
    public class PortfolioItemController : Controller
    {
        private readonly IRepository<PortfolioItem> _portfolioItemRepository;
        private readonly IRepository<PortfolioItemLink> _portfolioItemLinkRepository;
        private readonly AuthenticationService _authenticationService;

        public PortfolioItemController(IRepository<PortfolioItem> portfolioItemRepository, IRepository<PortfolioItemLink> portfolioItemLinkRepository)
        {
            _portfolioItemRepository = portfolioItemRepository;
            _portfolioItemLinkRepository = portfolioItemLinkRepository;
            _authenticationService = new AuthenticationService(new UserRepository(portfolioItemRepository.DatabaseInfo.Context));
        }
        
        [HttpGet]
        public IEnumerable<PortfolioItem> GetPublished()
        {
            var published = _portfolioItemRepository.GetAll().Where(x => x.Published);
            return published;
        }

        [HttpGet("admin", Name = "AdminGetAllPortfolioItems")]
        public IActionResult GetAllPortfolioItems([FromHeader(Name = "Authorization")] string authToken)
        {
            // Verify the Authorization Token
            if (!_authenticationService.VerifyAuthToken(authToken))
                return BadRequest("Invalid AuthToken");

            return new ObjectResult(_portfolioItemRepository.GetAll());
        }

        [HttpGet("{id}", Name = "GetPortfolioItem")]
        public IActionResult GetById(int id)
        {
            var item = _portfolioItemRepository.Find(id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromHeader(Name = "Authorization")] string authToken, [FromBody] PortfolioItem item)
        {
            if (!_authenticationService.VerifyAuthToken(authToken))
                return BadRequest("Invalid AuthToken");

            if (item == null)
                return BadRequest();

            item.Created = DateTime.UtcNow;
            item.Modified = DateTime.UtcNow;

            _portfolioItemRepository.AddAndCommit(item);
            return CreatedAtRoute("GetPortfolioItem", new { id = item.ID }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromHeader(Name = "Authorization")] string authToken, [FromBody] PortfolioItem item)
        {
            if (!_authenticationService.VerifyAuthToken(authToken))
                return BadRequest("Invalid AuthToken");

            if (item == null || item.ID != id)
                return BadRequest();

            var repoItem = _portfolioItemRepository.Find(id);
            if (repoItem == null)
                return NotFound();

            repoItem.Title = item.Title;
            repoItem.CoverImageUrl = item.CoverImageUrl;
            repoItem.SourceCodeUrl = item.SourceCodeUrl;
            repoItem.Published = item.Published;

            repoItem.Modified = DateTime.UtcNow;

            repoItem.Awards = item.Awards;
            repoItem.MyRole = item.MyRole;
            repoItem.Description = item.Description;
            repoItem.Features = item.Features;

            repoItem.Links.Clear();
            repoItem.Tags.Clear();
            repoItem.RelatedItems.Clear();
            _portfolioItemRepository.UpdateAndCommit(repoItem);

            repoItem.Links = item.Links;
            repoItem.Tags = item.Tags;
            repoItem.RelatedItems = item.RelatedItems;
            _portfolioItemRepository.UpdateAndCommit(repoItem);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromHeader(Name = "Authorization")] string authToken)
        {
            if (!_authenticationService.VerifyAuthToken(authToken))
                return BadRequest("Invalid AuthToken");

            var repoItem = _portfolioItemRepository.Find(id);
            if (repoItem == null)
                return NotFound();

            _portfolioItemRepository.RemoveAndCommit(id);
            return new NoContentResult();
        }
    }
}