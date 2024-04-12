using System.Threading.Tasks;
using BlogApp.Data;
using BlogApp.Models;
using BlogApp.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class CommentManagerTests : IAsyncLifetime
{
    private readonly AppDbContext _dbContext;
    private readonly CommentManager _commentManager;

    public CommentManagerTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BlogAppDb")
            .Options;

        _dbContext = new AppDbContext(options);
        _commentManager = new CommentManager(_dbContext);
    }

    public async Task InitializeAsync()
    {
        await _dbContext.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbContext.Database.EnsureDeletedAsync();
    }

    [Fact]
    public void CreateComment_ShouldSaveComment()
    {
        // Arrange
        var postId = 1;
        var commentText = "This is a test comment.";

        // Act
        var comment = _commentManager.CreateComment(postId, commentText);

        // Assert
        Assert.NotNull(comment);
        Assert.Equal(postId, comment.PostId);
        Assert.Equal(commentText, comment.Text);
        Assert.Equal(1, _dbContext.Comments.Count());
    }

    [Fact]
    public void GetCommentById_ShouldReturnComment()
    {
        // Arrange
        var postId = 1;
        var commentText = "Test comment";
        var comment = _commentManager.CreateComment(postId, commentText);

        // Act
        var result = _commentManager.GetCommentById(comment.CommentId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(comment.CommentId, result.CommentId);
    }

    [Fact]
    public void UpdateComment_ShouldUpdateComment()
    {
        // Arrange
        var postId = 1;
        var originalCommentText = "Original comment text";
        var comment = _commentManager.CreateComment(postId, originalCommentText);

        var updatedCommentText = "Updated comment text";

        // Act
        var updatedComment = _commentManager.UpdateComment(comment.CommentId, updatedCommentText);

        // Assert
        Assert.NotNull(updatedComment);
        Assert.Equal(updatedCommentText, updatedComment.Text);
        Assert.Equal(1, _dbContext.Comments.Count());
    }

    [Fact]
    public void DeleteComment_ShouldDeleteComment()
    {
        // Arrange
        var postId = 1;
        var commentText = "Test comment";
        var comment = _commentManager.CreateComment(postId, commentText);

        // Act
        var deleted = _commentManager.DeleteComment(comment.CommentId);

        // Assert
        Assert.True(deleted);
        Assert.Equal(0, _dbContext.Comments.Count());
    }
}