using CapstoneWIE.DataLayer.Factories;
using CapstoneWIE.DataLayer.Interfaces;
using CapstoneWIE.DataLayer.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CapstoneWIE.Controllers.ApiControllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : ApiController
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public AdminController()
        {
            _blogPostRepository = BlogPostRepositoryFactory.GetRepository();
        }

        [HttpGet]
        public IEnumerable<BlogPost> Get()
        {
            return _blogPostRepository.GetPendingPostsByDate();
        }
    }
}
