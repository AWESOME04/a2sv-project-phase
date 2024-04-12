using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Data;
using BlogApp.Models;
using BlogApp.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BlogApp.Tests
{
    [Collection("DatabaseCollection")]
    public class PostManagerTests : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _fixture;

        public PostManagerTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CreatePost_ShouldAddNewPostToDatabase()
        {
            _fixture.ResetDatabase();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "BlogAppTest")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var postManager = new PostManager(context);

                var title = "Test Post";
                var content = "This is a test post.";

                // Act
                var createdPost = postManager.CreatePost(title, content);

                // Assert
                Assert.NotNull(createdPost);
                Assert.Equal(title, createdPost.Title);
                Assert.Equal(content, createdPost.Content);
                Assert.NotEqual(default(DateTime), createdPost.CreatedAt);

                // Debugging
                Console.WriteLine($"Created Post Id: {createdPost.PostId}");

                // Check database state
                var allPosts = context.Posts.ToList();
                Console.WriteLine($"Number of Posts in Database: {allPosts.Count}");
                foreach (var post in allPosts)
                {
                    Console.WriteLine($"Post Id: {post.PostId}, Title: {post.Title}, Content: {post.Content}");
                }

                Assert.Single(allPosts); // Ensure only one post exists in the database
                Assert.Equal(createdPost.PostId, allPosts[0].PostId); // Verify the post in the database matches the created post
            }
        }

        [Fact]
        public async Task GetAllPosts_ShouldReturnAllPosts()
        {
            _fixture.ResetDatabase();
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "BlogAppTest")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var posts = new List<PostEntity>
                {
                    new PostEntity { Title = "Post 1", Content = "Content 1", CreatedAt = DateTime.UtcNow },
                    new PostEntity { Title = "Post 2", Content = "Content 2", CreatedAt = DateTime.UtcNow }
                };

                context.Posts.AddRange(posts);
                context.SaveChanges();

                var postManager = new PostManager(context);

                // Act
                var allPosts = postManager.GetAllPosts();

                // Assert
                Assert.NotNull(allPosts);
                Assert.Equal(posts.Count, allPosts.Count);
                Assert.Contains(posts[0], allPosts);
                Assert.Contains(posts[1], allPosts);

                // Debugging
                Console.WriteLine($"Number of Posts Retrieved: {allPosts.Count}");
                foreach (var post in allPosts)
                {
                    Console.WriteLine($"Post Id: {post.PostId}, Title: {post.Title}, Content: {post.Content}");
                }
            }
        }

        [Fact]
        public async Task GetPostById_ShouldReturnPostById()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "BlogAppTest")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var postManager = new PostManager(context);

                var post1 = new PostEntity { Title = "Post 1", Content = "Content 1", CreatedAt = DateTime.UtcNow };
                var post2 = new PostEntity { Title = "Post 2", Content = "Content 2", CreatedAt = DateTime.UtcNow };
                context.Posts.AddRange(post1, post2);
                context.SaveChanges();

                // Act
                var retrievedPost = postManager.GetPostById(post2.PostId);

                // Assert
                Assert.NotNull(retrievedPost);
                Assert.Equal(post2.PostId, retrievedPost.PostId);
                Assert.Equal(post2.Title, retrievedPost.Title);
                Assert.Equal(post2.Content, retrievedPost.Content);

                // Debugging
                Console.WriteLine($"Retrieved Post Id: {retrievedPost.PostId}, Title: {retrievedPost.Title}, Content: {retrievedPost.Content}");
            }
        }

        [Fact]
        public async Task UpdatePost_ShouldUpdatePost()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "BlogAppTest")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var postManager = new PostManager(context);

                var post = new PostEntity { Title = "Original Title", Content = "Original Content", CreatedAt = DateTime.UtcNow };
                context.Posts.Add(post);
                context.SaveChanges();

                var updatedTitle = "Updated Title";
                var updatedContent = "Updated Content";

                // Act
                var updatedPost = postManager.UpdatePost(post.PostId, updatedTitle, updatedContent);

                // Assert
                Assert.NotNull(updatedPost);
                Assert.Equal(updatedTitle, updatedPost.Title);
                Assert.Equal(updatedContent, updatedPost.Content);

                // Debugging
                Console.WriteLine($"Updated Post Id: {updatedPost.PostId}, Title: {updatedPost.Title}, Content: {updatedPost.Content}");
            }
        }

        [Fact]
        public async Task DeletePost_ShouldDeletePost()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "BlogAppTest")
                .Options;

            using (var context = new AppDbContext(options))
            {
                var postManager = new PostManager(context);

                var post = new PostEntity { Title = "Post to Delete", Content = "Content to Delete", CreatedAt = DateTime.UtcNow };
                context.Posts.Add(post);
                context.SaveChanges();

                // Act
                var deleteResult = postManager.DeletePost(post.PostId);

                // Assert
                Assert.True(deleteResult);

                // Check if post is deleted
                var deletedPost = context.Posts.Find(post.PostId);
                Assert.Null(deletedPost);

                // Debugging
                Console.WriteLine($"Post Deleted: {deleteResult}");
            }
        }
    }
}
