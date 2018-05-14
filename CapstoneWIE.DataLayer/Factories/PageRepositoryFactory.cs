using System.Configuration;
using CapstoneWIE.DataLayer.EfRepositories;
using CapstoneWIE.DataLayer.Interfaces;
using CapstoneWIE.DataLayer.Repositories;

namespace CapstoneWIE.DataLayer.Factories
{
    public static class PageRepositoryFactory
    {
        public static IPageRepository GetRepository()
        {
            var mode = ConfigurationManager.AppSettings["Mode"];

            switch (mode)
            {
                case "Dapper":
                    return new PageRepository();
                default:
                    return new EfPageRepository();
            }
        }
    }
}
