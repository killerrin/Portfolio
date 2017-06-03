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
    public class SearchController : Controller
    {
        private readonly IRepository<PortfolioItem> _portfolioItemRepository;
        private readonly IRepository<PortfolioItemLink> _portfolioItemLinkRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IRepository<TagType> _tagTypeRepository;

        private readonly AuthenticationService _authenticationService;

        public SearchController(IRepository<PortfolioItem> portfolioItemRepository, IRepository<PortfolioItemLink> portfolioItemLinkRepository, IRepository<Tag> tagRepository, IRepository<TagType> tagTypeRepository)
        {
            _portfolioItemRepository = portfolioItemRepository;
            _portfolioItemLinkRepository = portfolioItemLinkRepository;
            _tagRepository = tagRepository;
            _tagTypeRepository = tagTypeRepository;

            _authenticationService = new AuthenticationService(new UserRepository(portfolioItemRepository.DatabaseInfo.Context));
        }

        [HttpGet]
        public IEnumerable<PortfolioItem> Search([FromQuery]IEnumerable<string> searchTerms, [FromHeader(Name = "Authorization")] string authToken)
        {
            List<PortfolioItem> matchedItems = new List<PortfolioItem>();

            // Go through all the portfolio items and include all the matches
            List<PortfolioItem> allPortfolioItems;
            if (_authenticationService.VerifyAuthToken(authToken))
                allPortfolioItems = _portfolioItemRepository.GetAll().ToList();
            else
                allPortfolioItems = _portfolioItemRepository.GetAllQuery().Where(x => x.Published).ToList();


            foreach (var item in allPortfolioItems)
            {
                foreach (var term in searchTerms)
                {
                    if (ContainsKeyword(item, term) || ContainsTag(item, term))
                    {
                        matchedItems.Add(item);
                    }
                }
            }

            return matchedItems;
        }

        private bool ContainsKeyword(PortfolioItem item, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return false;

            return item.Title.ToLower().Contains(keyword.ToLower());
        }

        private bool ContainsTag(PortfolioItem item, string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                return false;

            foreach (var itemTag in item.Tags)
            {
                if (itemTag.Tag.Name.ToLower().Contains(tag.ToLower()))
                    return true;
            }

            return false;
        }
    }
}
