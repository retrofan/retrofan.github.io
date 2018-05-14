using CapstoneWIE.DataLayer.Factories;
using CapstoneWIE.DataLayer.Interfaces;
using CapstoneWIE.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CapstoneWIE.Controllers.ApiControllers
{
    public class PagesController : ApiController
    {
        private readonly IPageRepository _pageRepository;

        public PagesController()
        {
            _pageRepository = PageRepositoryFactory.GetRepository();
        }

        // /api/pages
        [HttpGet]
        public IEnumerable<Page> Get()
        {
            return _pageRepository.Get().ToList();
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var page = _pageRepository.Get(id);

            if (page == null)
                return NotFound();

            return Ok(page);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Post(Page page)
        {
            page.Id = 0;
            page.CreationDate = DateTime.Now;

            if (!ModelState.IsValid)
                return BadRequest();

            page.Id = _pageRepository.Add(page);

            return Created(new Uri(Request.RequestUri + "/" + page.Id), page);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Delete(int id)
        {
            _pageRepository.Delete(id);
            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult Put(Page page)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var PageInDb = _pageRepository.Get(page.Id);

            if (PageInDb == null)
                return NotFound();

            PageInDb = page;

            _pageRepository.Update(PageInDb);

            return Ok();
        }
    }
}
