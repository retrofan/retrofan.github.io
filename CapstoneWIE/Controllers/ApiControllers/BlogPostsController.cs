using CapstoneWIE.DataLayer.Factories;
using CapstoneWIE.DataLayer.Interfaces;
using CapstoneWIE.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CapstoneWIE.Controllers.ApiControllers
{
    public class BlogPostsController : ApiController
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogPostsController()
        {
            _blogPostRepository = BlogPostRepositoryFactory.GetRepository();
        }

        // for Moq unit tests
        public BlogPostsController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        // /api/blogposts
        [HttpGet]
        public IEnumerable<BlogPost> Get()
        {
            return _blogPostRepository.GetApprovedPostsByDate().Take(5);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var blogPost = _blogPostRepository.GetById(id);

            if (blogPost == null)
                return NotFound();

            return Ok(blogPost);
        }

        [HttpPost]
        public IHttpActionResult Post(BlogPost newBlog)
        {
            newBlog.Id = 0;
            newBlog.PostDate = DateTime.Now;

            if (!ModelState.IsValid)    //if model title/content are empty OR title/content are too long
                return BadRequest();

            newBlog.Id = _blogPostRepository.Add(newBlog);

            return Created(new Uri(Request.RequestUri + "/" + newBlog.Id), newBlog);
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var postInDb = _blogPostRepository.GetById(id);

            if (postInDb == null)
                return NotFound();

            _blogPostRepository.Delete(id);
            
            return Ok();
        }

        [Authorize(Roles = "Author, Admin")]
        [HttpPut]
        public IHttpActionResult Put(BlogPost blogPost)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var blogInDb = _blogPostRepository.GetById(blogPost.Id);

            if (blogInDb == null)
                return NotFound();

            blogInDb.BlogState = blogPost.BlogState;
            blogInDb.Id = blogPost.Id;
            blogInDb.Content = blogPost.Content;
            blogInDb.Title = blogPost.Title;
            blogInDb.Tags = blogPost.Tags;

            _blogPostRepository.Update(blogInDb);

            return Ok();
        }
    }
}
