using System;
using System.Collections.Generic;
using System.Linq;
using BlogApp.Data;
using BlogApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Services
{
    public class PostManager
    {
        private readonly AppDbContext _dbContext;

        public PostManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public PostEntity CreatePost(string title, string content)
        {
            var post = new PostEntity
            {
                Title = title,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();
            return post;
        }

        public List<PostEntity> GetAllPosts()
        {
            return _dbContext.Posts.ToList();
        }

        public PostEntity GetPostById(int postId)
        {
            return _dbContext.Posts.Find(postId);
        }

        public PostEntity GetPostByIdWithComments(int postId)
        {
            return _dbContext.Posts
                .Include(p => p.Comments)
                .FirstOrDefault(p => p.PostId == postId);
        }

        public PostEntity UpdatePost(int postId, string title, string content)
        {
            var post = _dbContext.Posts.Find(postId);
            if (post == null)
            {
                return null;
            }

            post.Title = title;
            post.Content = content;
            _dbContext.Posts.Update(post);
            _dbContext.SaveChanges();
            return post;
        }

        public bool DeletePost(int postId)
        {
            var post = _dbContext.Posts.Find(postId);
            if (post == null)
            {
                return false;
            }

            _dbContext.Posts.Remove(post);
            _dbContext.SaveChanges();
            return true;
        }
    }
}