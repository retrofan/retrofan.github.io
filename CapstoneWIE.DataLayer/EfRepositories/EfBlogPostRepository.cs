using CapstoneWIE.DataLayer.Interfaces;
using CapstoneWIE.DataLayer.Models;
using CapstoneWIE.DataLayer.Models.Enums;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CapstoneWIE.DataLayer.EfRepositories
{
    public class EfBlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext _context;

        public EfBlogPostRepository()
        {
            _context = new ApplicationDbContext();
        }

        // only exists for moq unit tests
        public IQueryable<BlogPost> BlogPosts { get; }

        public IEnumerable<BlogPost> Get()
        {
            return _context.BlogPosts.Include(b => b.ApplicationUser).Include(b => b.Tags).ToList();
        }

        public IEnumerable<BlogPost> GetPendingPostsByDate()
        {
            return _context.BlogPosts.Where(b => b.BlogState == BlogState.Pending)
                                     .OrderByDescending(b => b.PostDate)
                                     .Include(b => b.ApplicationUser)
                                     .Include(b => b.Tags)
                                     .ToList();
        }

        public IEnumerable<BlogPost> GetDraftAndPendingByUserOrderByDate(string id)
        {
            return Get().Where(b => b.ApplicationUserId == id &&
                        (b.BlogState == BlogState.Draft || b.BlogState == BlogState.Pending))
                        .OrderByDescending(b => b.PostDate);
        }

        public IEnumerable<BlogPost> GetApprovedPostsByDate()
        {
            return Get().Where(b => b.BlogState == BlogState.Approved).OrderByDescending(b => b.PostDate);
        }

        public BlogPost GetById(int id)
        {
            return _context.BlogPosts.Include(b => b.ApplicationUser).Include(b => b.Tags).SingleOrDefault(b => b.Id == id);
        }

        public int Add(BlogPost post)
        {
            _context.BlogPosts.Add(post);
            _context.SaveChanges();
            return post.Id;
        }

        public void Delete(int id)
        {
            var blogPost = _context.BlogPosts.SingleOrDefault(b => b.Id == id);

            _context.BlogPosts.Remove(blogPost);
            _context.SaveChanges();
        }

        public void Update(BlogPost blogInDb)
        {
            _context.SaveChanges();
        }
    }
}
