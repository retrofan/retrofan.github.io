using CapstoneWIE.DataLayer.Models;
using Dapper;
using NUnit.Framework;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace CapstoneWIE.Tests
{
    [TestFixture]
    public class BlogPostsTests
    {
        private string _connectionString;

        [OneTimeSetUp]
        public void Init()
        {
            #region 

            var query = @"--alter table blogpost drop constraint [FK_dbo.BlogPost_dbo.AspNetUsers_ApplicationUserId]
--alter table blogpost drop constraint [FK_dbo.BlogPost_dbo.AspNetUsers_ApplicationUser_Id]

alter table tagblogpost drop constraint [FK_dbo.TagBlogPost_dbo.BlogPost_BlogPost_Id]
alter table tagblogpost drop constraint [FK_dbo.TagBlogPost_dbo.Tag_Tag_Id]


truncate table blogpost


truncate table tag


truncate table tagblogpost

USE [CapstoneDBTest]


ALTER TABLE [dbo].[TagBlogPost]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TagBlogPost_dbo.BlogPost_BlogPost_Id] FOREIGN KEY([BlogPost_Id])
REFERENCES [dbo].[BlogPost] ([Id])
ON DELETE CASCADE


ALTER TABLE [dbo].[TagBlogPost] CHECK CONSTRAINT [FK_dbo.TagBlogPost_dbo.BlogPost_BlogPost_Id]


USE [CapstoneDBTest]


ALTER TABLE [dbo].[TagBlogPost]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TagBlogPost_dbo.Tag_Tag_Id] FOREIGN KEY([Tag_Id])
REFERENCES [dbo].[Tag] ([Id])
ON DELETE CASCADE


ALTER TABLE [dbo].[TagBlogPost] CHECK CONSTRAINT [FK_dbo.TagBlogPost_dbo.Tag_Tag_Id]


USE [CapstoneDBTest]

SET IDENTITY_INSERT [dbo].[BlogPost] ON 


INSERT [dbo].[BlogPost] ([Id], [Title], [Content], [PostDate], [ApplicationUserId]) VALUES (1, N'test', N'<p>testtesttest</p>', CAST(N'2016-01-01 00:00:00.000' AS DateTime), N'6a8326da-0056-4ec9-a2ae-b22875a442ed')

INSERT [dbo].[BlogPost] ([Id], [Title], [Content], [PostDate], [ApplicationUserId]) VALUES (2, N'biggerTest', N'<p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem.</p>

<p>Nulla consequat massa quis enim. Donec pede justo, fringilla vel, aliquet nec, vulputate eget, arcu. In enim justo, rhoncus ut, imperdiet a, venenatis vitae, justo. Nullam dictum felis eu pede mollis pretium. Integer tincidunt. Cras dapibus.</p>

<p>Vivamus elementum semper nisi. Aenean vulputate eleifend tellus. Aenean leo ligula, porttitor eu, consequat vitae, eleifend ac, enim. Aliquam lorem ante, dapibus in, viverra quis, feugiat a, tellus. Phasellus viverra nulla ut metus varius laoreet. Quisque rutrum. Aenean imperdiet. Etiam ultricies nisi vel augue. Curabitur ullamcorper ultricies nisi. Nam eget dui. Etiam rhoncus. Maecenas tempus, tellus eget condimentum rhoncus, sem quam semper libero, sit amet adipiscing sem neque sed ipsum. N</p>', CAST(N'2016-01-01 00:00:00.000' AS DateTime), N'6a8326da-0056-4ec9-a2ae-b22875a442ed')

SET IDENTITY_INSERT [dbo].[BlogPost] OFF

SET IDENTITY_INSERT [dbo].[Tag] ON 


INSERT [dbo].[Tag] ([Id], [Name]) VALUES (1, N'test')

INSERT [dbo].[Tag] ([Id], [Name]) VALUES (2, N'willstest')

INSERT [dbo].[Tag] ([Id], [Name]) VALUES (3, N'isaacstest')

INSERT [dbo].[Tag] ([Id], [Name]) VALUES (4, N'erikstest')

SET IDENTITY_INSERT [dbo].[Tag] OFF

INSERT [dbo].[TagBlogPost] ([Tag_Id], [BlogPost_Id]) VALUES (1, 1)

INSERT [dbo].[TagBlogPost] ([Tag_Id], [BlogPost_Id]) VALUES (1, 2)

INSERT [dbo].[TagBlogPost] ([Tag_Id], [BlogPost_Id]) VALUES (2, 1)

INSERT [dbo].[TagBlogPost] ([Tag_Id], [BlogPost_Id]) VALUES (2, 2)

INSERT [dbo].[TagBlogPost] ([Tag_Id], [BlogPost_Id]) VALUES (3, 1)

INSERT [dbo].[TagBlogPost] ([Tag_Id], [BlogPost_Id]) VALUES (4, 1)
";
#endregion
            _connectionString = ConfigurationManager.ConnectionStrings["CapstoneDBTest"].ConnectionString;

            using (var cn = new SqlConnection(_connectionString))
            {
                cn.Execute(query);
            }
        }

        [Test]
        public void DoesDataBaseContainData()
        {
            var query = "select * from blogpost";
            List<BlogPost> blogPosts;

            using (var cn  = new SqlConnection(_connectionString))
            {
                blogPosts = cn.Query<BlogPost>(query).ToList();
            }

            Assert.IsNotNull(blogPosts);
        }

        [Test]
        public void CanGetBlogPostById()
        {
            var query = "select * from blogpost where id = 1";
            BlogPost blogPost;

            using (var cn = new SqlConnection(_connectionString))
            {
                blogPost = cn.Query<BlogPost>(query).SingleOrDefault();
            }

            Assert.IsNotNull(blogPost);
        }

        [Test]
        public void TryToGetNonExistingIdFails()
        {
            var query = "select * from blogpost where id = 123";
            BlogPost blogPost;

            using (var cn = new SqlConnection(_connectionString))
            {
                blogPost = cn.Query<BlogPost>(query).SingleOrDefault();
            }

            Assert.IsNull(blogPost);
        }
    }
}
