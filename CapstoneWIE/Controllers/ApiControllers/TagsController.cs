using CapstoneWIE.DataLayer.Factories;
using CapstoneWIE.DataLayer.Interfaces;
using CapstoneWIE.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CapstoneWIE.Controllers.ApiControllers
{
    public class TagsController : ApiController
    {
        private readonly ITagRepository _tagRepository;

        public TagsController()
        {
            _tagRepository = TagRepositoryFactory.GetRepository();
        }

        // /api/tags
        [HttpGet]
        public IEnumerable<Tag> Get()
        {
            return _tagRepository.Get();
        }

        // /api/tags/id
        [HttpGet]
        public IEnumerable<Tag> GetTagsByBlogPost(int id)
        { 
            return _tagRepository.GetTagsByBlogPost(id).ToList();
        }

        [HttpPost]
        [Authorize(Roles = "Author, Admin")]
        public IHttpActionResult Post(int blogId, Tag tag)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            tag.Id = _tagRepository.AddTagToBlogPost(blogId, tag);

            return Created(new Uri(Request.RequestUri + "/" + tag.Id), tag);
        }
    }
}
