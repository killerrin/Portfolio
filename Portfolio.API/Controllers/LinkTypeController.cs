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
using Portfolio.API.Extensions;
using Portfolio.API.Models.Enums;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Portfolio.API.Controllers
{
    [Route("api/[controller]")]
    //[EnableCors("CorsEverythingPolicy")]
    public class LinkTypeController : Controller
    {
        //private readonly IRepository<LinkType> _linkTypeRepository;
        //private readonly AuthenticationService _authenticationService;

        public LinkTypeController()//IRepository<TagType> linkTypeRepository)
        {
            //_linkTypeRepository = linkTypeRepository;
            //_authenticationService = new AuthenticationService(new UserRepository(linkTypeRepository.DatabaseInfo.Context));
        }

        [HttpGet]
        public IEnumerable<EnumModel> GetAll()
        {
            return EnumExtensions.GetNameValuePair<LinkType>();
        }

        [HttpGet("{id}", Name = "GetLinkType")]
        public IActionResult GetById(int id)
        {
            if (Enum.TryParse<LinkType>(id.ToString(), out LinkType linkType))
            {
                EnumModel item = new EnumModel(linkType);
                if (!item.Name.Equals(item.Value.ToString()))
                    return new ObjectResult(item);
            }
            return NotFound();
        }

        //[HttpPost]
        //public IActionResult Create([Required][FromHeader(Name = "Authorization")] string authToken, [FromBody] TagType item)
        //{
        //    if (!_authenticationService.VerifyAuthToken(authToken))
        //        return BadRequest("Invalid AuthToken");

        //    if (item == null)
        //        return BadRequest();

        //    _linkTypeRepository.AddAndCommit(item);
        //    return CreatedAtRoute("GetLinkType", new { id = item.ID }, item);
        //}

        //[HttpPut("{id}")]
        //public IActionResult Update(int id, [Required][FromHeader(Name = "Authorization")] string authToken, [FromBody] TagType item)
        //{
        //    if (!_authenticationService.VerifyAuthToken(authToken))
        //        return BadRequest("Invalid AuthToken");

        //    if (item == null || item.ID != id)
        //        return BadRequest();

        //    var repoItem = _linkTypeRepository.Find(id);
        //    if (repoItem == null)
        //        return NotFound();

        //    repoItem.Name = item.Name;

        //    _linkTypeRepository.UpdateAndCommit(repoItem);

        //    return new NoContentResult();
        //}

        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id, [Required][FromHeader(Name = "Authorization")] string authToken)
        //{
        //    if (!_authenticationService.VerifyAuthToken(authToken))
        //        return BadRequest("Invalid AuthToken");

        //    var repoItem = _linkTypeRepository.Find(id);
        //    if (repoItem == null)
        //        return NotFound();

        //    _linkTypeRepository.RemoveAndCommit(id);
        //    return new NoContentResult();
        //}
    }
}