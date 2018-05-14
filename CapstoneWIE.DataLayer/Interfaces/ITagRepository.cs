using System.Collections.Generic;
using CapstoneWIE.DataLayer.Models;

namespace CapstoneWIE.DataLayer.Interfaces
{
    public interface ITagRepository
    {
        IEnumerable<Tag> Get();
        IEnumerable<Tag> GetTagsByBlogPost(int id);
        int AddTagToBlogPost(int blogId, Tag tag);
    }
}