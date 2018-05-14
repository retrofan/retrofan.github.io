using CapstoneWIE.DataLayer.Factories;
using CapstoneWIE.DataLayer.Interfaces;
using CapstoneWIE.DataLayer.Models;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CapstoneWIE.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogController()
        {
            _blogPostRepository = BlogPostRepositoryFactory.GetRepository();
        }

        // GET: Blog
        public ActionResult BlogDetails(int id)
        {
            var blogPost = _blogPostRepository.GetById(id);

            if (blogPost == null)
            {
                blogPost = new BlogPost
                {
                    Title = "404 Blog Not Found",
                    Content = "",
                    Tags = new List<Tag>()
                };
            }
            return View(blogPost);
        }


        [Authorize(Roles = "Author, Admin")]
        public ActionResult AuthorHome()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ViewPendingPosts()
        {
            return View();
        }

        [Authorize(Roles = "Author, Admin")]
        public ActionResult AddBlog()
        {
            return View();
        }

        [Authorize(Roles = "Admin, Author")]
        public ActionResult EditBlog(int id)
        {
            var blog = _blogPostRepository.GetById(id);

            if(blog.ApplicationUser.Id == User.Identity.GetUserId())
                return View(blog);

            return RedirectToAction("AuthorHome", "Blog");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateStaticPage()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Games()
        {
            return View();
        }
    }
}