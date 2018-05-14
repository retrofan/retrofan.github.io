using System.Configuration;
using CapstoneWIE.DataLayer.EfRepositories;
using CapstoneWIE.DataLayer.Interfaces;
using CapstoneWIE.DataLayer.Repositories;

namespace CapstoneWIE.DataLayer.Factories
{
    public static class BlogPostRepositoryFactory
    {
        public static IBlogPostRepository GetRepository()
        {
            var mode = ConfigurationManager.AppSettings["Mode"];

            switch (mode)
            {
                case "Dapper":
                    return new BlogPostRepository();
                default:
                    return new EfBlogPostRepository();
            }
        }
    }
}
