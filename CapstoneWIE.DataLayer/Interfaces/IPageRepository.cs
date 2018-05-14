using System.Collections.Generic;
using CapstoneWIE.DataLayer.Models;

namespace CapstoneWIE.DataLayer.Interfaces
{
    public interface IPageRepository
    {
        IEnumerable<Page> Get();
        Page Get(int id);
        int Add(Page page);
        void Delete(int id);
        void Update(Page page);
    }
}