using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using Blog.DAL.Infrastructure;
using Blog.DAL.Model;
using Blog.DAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using TDD.DbTestHelpers.Yaml;
using TDD.DbTestHelpers.Core;
using System.Collections.Generic;

namespace Blog.DAL.Tests
{
    public class BlogFixtures : YamlDbFixture<BlogContext, BlogFixturesModel>
    {
        public BlogFixtures()
        {
            SetYamlFiles("posts.yml");
        }
    }

    public class BlogFixturesModel
    {
        public FixtureTable<Post> Posts { get; set; }
        public FixtureTable<Comment> Comments { get; set; }
    }

    //public class DbBaseTest<BlogFixtures>

    [TestClass]
    public class RepositoryTests : DbBaseTest<BlogFixtures>
    {
        [TestInitialize]
        public void TestsInit()
        {
            
        }

        [TestMethod]
        public void GetAllPost_OnePostInDb_ReturnOnePost()
        {
            // arrange
            /*var context = new BlogContext();
            context.Database.CreateIfNotExists();*/
            var repository = new BlogRepository();
            
            // -- prepare data in db
            /*context.Posts.ToList().ForEach(x => context.Posts.Remove(x));
            context.Posts.Add(new Post
            {
                Author = "test",
                Content = "test, test, test..."
            });
            context.SaveChanges();
            */
            // act
            var result = repository.GetAllPosts();
            // assert
            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void GetAllPost_AddPost_TwoPostsExpected()
        {
            // arrange
            /*var context = new BlogContext();
            context.Database.CreateIfNotExists();*/
            var repository = new BlogRepository();

            repository.AddPost(new Post()
            {
                Content = "blabla",
                Author = "autor"
            });
            // act
            var result = repository.GetAllPosts();
            // assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void GetAllPost_AddPostWithoutAuthor_ValidatePost()
        {
            // arrange
            /*var context = new BlogContext();
            context.Database.CreateIfNotExists();*/
            var repository = new BlogRepository();

            repository.AddPost(new Post()
            {
                Content = "blabla",
                Author = null
            });
            // act
            var result = repository.GetAllPosts();
            // assert
            Assert.AreEqual(2, result.Count());
        }
    
        [TestMethod]
        public void GetComments_AddPostAndComment_OneCommentExpected()
        {
            // arrange
            var repository = new BlogRepository();

            repository.AddPost(new Post()
            {
                Content = "blabla",
                Author = "ja"
            });

            var lista = repository.GetAllPosts();
            
            
            repository.AddComment(new Comment()
            {
                Content = "aha",
                PostId = lista.First().Id
            });
            // act
            var result = repository.GetCommentsToPost(lista.First().Id);
            // assert
            Assert.AreEqual(1, result.Count());
        }
    }
}
