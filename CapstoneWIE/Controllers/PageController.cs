using CapstoneWIE.DataLayer.Factories;
using CapstoneWIE.DataLayer.Interfaces;
using CapstoneWIE.DataLayer.Models;
using System;
using System.Web.Mvc;

namespace CapstoneWIE.Controllers
{
    public class PageController : Controller
    {
        private readonly IPageRepository _pageRepository;

        public PageController()
        {
            _pageRepository = PageRepositoryFactory.GetRepository();
        }

        public ActionResult PageTemplate(int id)
        {
            var page = _pageRepository.Get(id);

            if (page == null)
            {
                page = new Page
                {
                    Content = "404 not found",
                    CreationDate = new DateTime(1999, 12, 31),
                    Title = "404 no title"
                };
            }

            return View(page);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult PageEdit(int id)
        {
            var page = _pageRepository.Get(id);
            return View(page);
        }
    }
}