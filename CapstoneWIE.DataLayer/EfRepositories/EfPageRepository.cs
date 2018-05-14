using CapstoneWIE.DataLayer.Interfaces;
using CapstoneWIE.DataLayer.Models;
using System.Collections.Generic;
using System.Linq;

namespace CapstoneWIE.DataLayer.EfRepositories
{
    class EfPageRepository : IPageRepository
    {
        private readonly ApplicationDbContext _context;

        public EfPageRepository()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<Page> Get()
        {
            return _context.Pages.ToList();
        }

        public Page Get(int id)
        {
            return _context.Pages.SingleOrDefault(p => p.Id == id);
        }

        public int Add(Page page)
        {
            _context.Pages.Add(page);
            _context.SaveChanges();
            return page.Id;
        }

        public void Delete(int id)
        {
            var page = _context.Pages.SingleOrDefault(p => p.Id == id);
            _context.Pages.Remove(page);
            _context.SaveChanges();
        }

        public void Update(Page page)
        {
            _context.SaveChanges();
        }
    }
}
