using BlogApp.Data;
using BlogApp.Models;

public class CommentManager
{
    private readonly AppDbContext _dbContext;

    public CommentManager(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public CommentEntity CreateComment(int postId, string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentException("Comment text cannot be empty.");
        }

        var comment = new CommentEntity
        {
            PostId = postId,
            Text = text,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.Comments.Add(comment);
        _dbContext.SaveChanges();
        return comment;
    }


    public CommentEntity GetCommentById(int commentId)
    {
        return _dbContext.Comments.FirstOrDefault(c => c.CommentId == commentId);
    }

    public CommentEntity UpdateComment(int commentId, string newText) // Change parameter name to 'newText'
    {
        var comment = _dbContext.Comments.Find(commentId);
        if (comment == null)
        {
            return null;
        }

        comment.Text = newText;
        _dbContext.SaveChanges();
        return comment;
    }


    public bool DeleteComment(int commentId)
    {
        var comment = _dbContext.Comments.Find(commentId);
        if (comment == null)
        {
            return false;
        }

        _dbContext.Comments.Remove(comment);
        _dbContext.SaveChanges();
        return true;
    }
}
