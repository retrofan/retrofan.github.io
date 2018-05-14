using CapstoneWIE.DataLayer.Interfaces;
using CapstoneWIE.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CapstoneWIE.DataLayer.EfRepositories
{
    public class EfTagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        public EfTagRepository()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<Tag> Get()
        {
            return _context.Tags.Include(t => t.BlogPosts).ToList();
        }

        public IEnumerable<Tag> GetTagsByBlogPost(int id)
        {
            var blogPost = _context.BlogPosts.Include(b => b.Tags).SingleOrDefault(b => b.Id == id);
            return blogPost.Tags;
        }

        public int AddTagToBlogPost(int blogId, Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}
