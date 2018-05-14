using CapstoneWIE.DataLayer.Config;
using CapstoneWIE.DataLayer.Interfaces;
using CapstoneWIE.DataLayer.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CapstoneWIE.DataLayer.Repositories
{
    public class PageRepository : IPageRepository
    {
        public IEnumerable<Page> Get()
        {
            var query = "select * from page";
            List<Page> pages;

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                pages = cn.Query<Page>(query).ToList();
            }

            return pages;
        }

        public Page Get(int id)
        {
            var query = "select * from page where id = @Id";
            Page page;

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("Id", id);

                page = cn.Query<Page>(query, p).SingleOrDefault();
            }

            return page;
        }

        public int Add(Page page)
        {
            var query = "insert into Page values (@Title, @Content, @ApplicationUserId, @CreationDate) set @Id = SCOPE_IDENTITY()";
            int idToReturn;

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("Title", page.Title);
                p.Add("Content", page.Content);
                p.Add("CreationDate", page.CreationDate);
                p.Add("ApplicationUserId", page.ApplicationUserId);
                p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

                cn.Execute(query, p);

                idToReturn = p.Get<int>("Id");
            }

            return idToReturn;
        }

        public void Delete(int id)
        {
            var query = "Delete from Page where Id = @Id";
            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("Id", id);
                cn.Query(query, p);
            }
        }

        public Page GetById(int id)
        {
            var query = "select * from page p where p.id = @id";
            Page page;

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("id", id);
                page = cn.Query<Page>(query).SingleOrDefault();
            }

            return page;
        }

        public void Update(Page pageInDb)
        {
            pageInDb.CreationDate = DateTime.Now;

            var query = "update page set creationdate = @creationdate, title = @Title, Content = @Content where id = @Id";

            using (var cn = new SqlConnection(Settings.ConnectionString))
            {
                var p = new DynamicParameters();
                p.Add("Id", pageInDb.Id);
                p.Add("creationdate", pageInDb.CreationDate);
                p.Add("Title", pageInDb.Title);
                p.Add("Content", pageInDb.Content);

                cn.Execute(query, p);
            }
        }
    }
}
