using CapstoneWIE.Controllers.ApiControllers;
using CapstoneWIE.DataLayer.Interfaces;
using CapstoneWIE.DataLayer.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;

namespace CapstoneWIE.Tests
{
    [TestFixture]
    public class BlogPostsControllerTests
    {
        private Mock<IBlogPostRepository> _mockRepository;
        private BlogPostsController _controller;
        private List<BlogPost> _blogPosts;

        [SetUp]
        public void TestInit()
        {
            _mockRepository = new Mock<IBlogPostRepository>();
            _blogPosts = new List<BlogPost>();
            _mockRepository.Setup(b => b.BlogPosts).Returns(_blogPosts.AsQueryable());
            _mockRepository.Setup(b => b.GetById(1)).Returns(new BlogPost { Id = 1 });

            _controller = new BlogPostsController(_mockRepository.Object);
        }

        [Test]
        public void CanGetBlogPosts()
        {
            var result = _controller.Get();

            Assert.IsNotNull(result);
        }

        [Test]
        public void GetNonExistingIdFails()
        {
            var result = _controller.Get(1231121);

            Console.WriteLine(result);

            Assert.That(result.GetType() == typeof(NotFoundResult));
        }

        [Test] // reminder bad request test
        public void GetExistingIdReturnsOk()
        {
            var result = _controller.Get(1);

            Console.WriteLine(result);

            Assert.IsNotNull(result);
            Assert.That(result.GetType() == typeof(OkNegotiatedContentResult<BlogPost>));
        }
    }
}
