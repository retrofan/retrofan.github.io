using CapstoneWIE.DataLayer.Interfaces;
using CapstoneWIE.DataLayer.Models;
using CapstoneWIE.DataLayer.Models.Enums;
using Dapper;
using Dapper.Mapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CapstoneWIE.DataLayer.Config;

namespace CapstoneWIE.DataLayer.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly TagRepository _tagRepository;

        public BlogPostRepository()
        {
            _tagRepository = new TagRepository();
        }

        public IQueryable<BlogPost> BlogPosts { get; }

        public IEnumerable<BlogPost> Get()
        {
            var storedProc = "GetBlogPostsWithAuthors";

            List<BlogPost> blogPosts;

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                blogPosts = cn.Query<BlogPost, ApplicationUser>(storedProc, commandType: CommandType.StoredProcedure).ToList();

                foreach (var blogPost in blogPosts)
                {
                    blogPost.Tags = _tagRepository.GetTagsByBlogPost(blogPost.Id).ToList();
                }
            }

            return blogPosts;
        }

        public IEnumerable<BlogPost> GetPendingPostsByDate()
        {
            return Get().Where(b => b.BlogState == BlogState.Pending).OrderByDescending(b => b.PostDate);
        }

        public IEnumerable<BlogPost> GetDraftAndPendingByUserOrderByDate(string id)
        {
            return Get().Where( b => b.ApplicationUserId == id && (b.BlogState == BlogState.Draft || b.BlogState == BlogState.Pending)).OrderByDescending(b => b.PostDate);
        }

        public IEnumerable<BlogPost> GetApprovedPostsByDate()
        {
            return Get().Where(b => b.BlogState == BlogState.Approved).OrderByDescending(b => b.PostDate);
        }

        public BlogPost GetById(int id)
        {
            var storedProc = "getBlogPostById";
            BlogPost blogPost;

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                blogPost = cn.Query<BlogPost, ApplicationUser>(storedProc, new { id }, commandType: CommandType.StoredProcedure).SingleOrDefault();
                if (blogPost != null)
                    blogPost.Tags = _tagRepository.GetTagsByBlogPost(blogPost.Id).ToList();
            }

            return blogPost;
        }

        public int Add(BlogPost post)
        {
            var proc = "AddBlogPost";
            int id;

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {

                var p = new DynamicParameters();
                p.Add("Title", post.Title); //the value you're assigning to the column
                p.Add("Content", post.Content);
                p.Add("PostDate", post.PostDate);
                p.Add("ApplicationUserId", post.ApplicationUserId);
                p.Add("BlogState", post.BlogState);
                p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

                cn.Execute(proc, p, commandType: CommandType.StoredProcedure);
                id = p.Get<int>("Id");

            }
            foreach (var t in post.Tags)
            {
                _tagRepository.AddTagToBlogPost(id, t);
            }
            return id;
        }

        public void Delete(int id)
        {
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var query = "delete from Blogpost where Id = @id";

                var p = new DynamicParameters();
                p.Add("Id", id);

                cn.Execute(query, p);

                cn.Query("delete Tag from TagBlogPost " +
                              "right outer join Tag " +
                              "on TagBlogPost.Tag_Id = Tag.Id " +
                              "where BlogPost_Id is NULL");
            }
        }

        public void Update(BlogPost blogInDb)
        {
            blogInDb.PostDate = DateTime.Now;
            var query = "update blogpost set blogstate = @blogstate, postdate = @postdate, title = @Title, Content = @Content where id = @Id"; // does not like the sql

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("Id", blogInDb.Id);
                p.Add("blogstate", blogInDb.BlogState);
                p.Add("postdate", blogInDb.PostDate);
                p.Add("Title", blogInDb.Title);
                p.Add("Content", blogInDb.Content);

                cn.Execute(query, p);
            }
            foreach (var tag in blogInDb.Tags)
            {
                _tagRepository.AddTagToBlogPost(blogInDb.Id, tag);
            }
        }
    }
}
