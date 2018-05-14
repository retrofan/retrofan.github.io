using System.Collections.Generic;
using System.Linq;
using CapstoneWIE.DataLayer.Models;

namespace CapstoneWIE.DataLayer.Interfaces
{
    public interface IBlogPostRepository
    {
        IQueryable<BlogPost> BlogPosts { get; }
        IEnumerable<BlogPost> Get();
        IEnumerable<BlogPost> GetPendingPostsByDate();
        IEnumerable<BlogPost> GetDraftAndPendingByUserOrderByDate(string id);
        IEnumerable<BlogPost> GetApprovedPostsByDate();
        BlogPost GetById(int id);
        int Add(BlogPost post); //
        void Delete(int id);
        void Update(BlogPost blogInDb);
    }
}