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
        public IEnumerable<PortfolioItem> Search([FromQuery]string tags)
        {
            var allPortfolioItems = _portfolioItemRepository.GetAll();
            List<PortfolioItem> matchedItems = new List<PortfolioItem>();

            foreach (var item in allPortfolioItems)
            {

            }

            return matchedItems;
        }
    }

    public class SearchTerms
    {
        public string Keyword { get; set; }
        public string Tag { get; set; }
        public bool Exclude { get; set; }
    }
}
