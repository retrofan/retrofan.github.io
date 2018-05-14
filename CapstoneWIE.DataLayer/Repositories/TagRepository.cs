using CapstoneWIE.DataLayer.Config;
using CapstoneWIE.DataLayer.Interfaces;
using CapstoneWIE.DataLayer.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CapstoneWIE.DataLayer.Repositories
{
    public class TagRepository : ITagRepository
    {
        public IEnumerable<Tag> Get()
        {
            var query = "select t.Id, t.Name from Tag t inner join TagBlogPost tb on t.Id = tb.Tag_Id group by t.Id, t.Name order by count(tb.BlogPost_Id) desc";

            List<Tag> tags;

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                tags = cn.Query<Tag>(query).ToList();
            }

            return tags;
        }

        public IEnumerable<Tag> GetTagsByBlogPost(int id)
        {
            var storedProc = "GetTagsByBlogPost";
            List<Tag> tags;

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                tags = cn.Query<Tag>(storedProc, new { id }, commandType: CommandType.StoredProcedure).ToList();
            }

            return tags;
        }

        public int AddTagToBlogPost(int blogId, Tag tag)
        {
            var tagId = 0;
            var tags = Get().ToList();
            var storedAddNewTag = "AddNewTag";
            var storedUpdateTag = "UpdateTagJunction";

            // if the name does not exist update tag table, if the name does exist get the id then update join table
            if (tags.Any(t => t.Name.ToUpper() == tag.Name.ToUpper()))
                tagId = tags.First(t => t.Name.ToUpper() == tag.Name.ToUpper()).Id;

            if (tags.All(t => t.Name != tag.Name))
            {
                using (var cn = new SqlConnection(Settings.ConnectionString))
                {
                    var p = new DynamicParameters();
                    p.Add("Name", tag.Name);
                    p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

                    cn.Execute(storedAddNewTag, p, commandType: CommandType.StoredProcedure);

                    tagId = p.Get<int>("Id");
                }
            }

            var blogsCurrentTags = GetTagsByBlogPost(blogId).ToList();
            if (blogsCurrentTags.All(t => t.Id != tagId))
            {
                using (var cn = new SqlConnection(Settings.ConnectionString))
                {
                    var p = new DynamicParameters();
                    p.Add("BlogPost_Id", blogId);
                    p.Add("Tag_Id", tagId);

                    cn.Execute(storedUpdateTag, p, commandType: CommandType.StoredProcedure);
                }
            }

            return tagId;
        }
    }
}
