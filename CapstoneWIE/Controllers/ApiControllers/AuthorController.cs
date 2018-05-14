using CapstoneWIE.DataLayer.Factories;
using CapstoneWIE.DataLayer.Interfaces;
using CapstoneWIE.DataLayer.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CapstoneWIE.Controllers.ApiControllers
{
    [Authorize(Roles = "Author, Admin")]
    public class AuthorController : ApiController
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public AuthorController()
        {
            _blogPostRepository = BlogPostRepositoryFactory.GetRepository();
        }

        [HttpGet]
        public IEnumerable<BlogPost> Get(string id)
        {
            return _blogPostRepository.GetDraftAndPendingByUserOrderByDate(id);
        }
    }
}
