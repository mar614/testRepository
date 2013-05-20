using System.Collections.Generic;
using Blog.DAL.Infrastructure;
using Blog.DAL.Model;
using System;
using System.Linq;

namespace Blog.DAL.Repository
{
    public class BlogRepository
    {
        private readonly BlogContext _context;

        public BlogRepository()
        {
            _context = new BlogContext();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts;
        }

        public void AddPost(Post post)
        {
            if (post.Author == null || post.Author == "")
                throw new ExecutionEngineException();
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        /// <summary>
        /// Gets comments to givent post
        /// </summary>
        /// <param name="postId">ID of post in dbase</param>
        /// <returns>List of comments</returns>
        public IEnumerable<Comment> GetCommentsToPost(long postId)
        {
            var lista = from c in _context.Comments where c.PostId == postId select c;
            return lista;
        }

        public void AddComment(Comment comment)
        {
            if (comment.PostId < 0)
                throw new ExecutionEngineException();
            _context.Comments.Add(comment);
            _context.SaveChanges();

        }
    }
}
