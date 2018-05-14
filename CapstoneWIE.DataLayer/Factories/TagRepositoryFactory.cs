using System.Configuration;
using CapstoneWIE.DataLayer.EfRepositories;
using CapstoneWIE.DataLayer.Interfaces;
using CapstoneWIE.DataLayer.Repositories;

namespace CapstoneWIE.DataLayer.Factories
{
    public static class TagRepositoryFactory
    {
        public static ITagRepository GetRepository()
        {
            var mode = ConfigurationManager.AppSettings["Mode"];

            switch (mode)
            {
                case "Dapper":
                    return new TagRepository();
                default:
                    return new EfTagRepository();
            }
        }
    }
}
