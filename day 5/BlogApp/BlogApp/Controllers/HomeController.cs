using BlogApp.Data;
using BlogApp.Models;
using BlogApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BlogApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PostManager _postManager;
        private readonly CommentManager _commentManager;
        private readonly AppDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, PostManager postManager, CommentManager commentManager, AppDbContext dbContext)
        {
            _logger = logger;
            _postManager = postManager;
            _commentManager = commentManager;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var posts = _postManager.GetAllPosts();
            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(string title, string content)
        {
            _postManager.CreatePost(title, content);
            return RedirectToAction("Index");
        }

        public IActionResult EditPost(int id)
        {
            var post = _postManager.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        public IActionResult UpdatePost(int id, string title, string content)
        {
            var updatedPost = _postManager.UpdatePost(id, title, content);
            if (updatedPost == null)
            {
                return NotFound();
            }
            return RedirectToAction("PostDetails", new { id = updatedPost.PostId });
        }

        public IActionResult DeletePost(int id)
        {
            var deleted = _postManager.DeletePost(id);
            if (!deleted)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }

        public PostEntity GetPostByIdWithComments(int postId)
        {
            return _dbContext.Posts
                .Include(p => p.Comments)
                .FirstOrDefault(p => p.PostId == postId);
        }

        public IActionResult PostDetails(int id)
        {
            var post = _postManager.GetPostByIdWithComments(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        [HttpPost]
        public IActionResult AddComment(int postId, string commentText)
        {
            try
            {
                _commentManager.CreateComment(postId, commentText);
                var updatedPost = _postManager.GetPostByIdWithComments(postId);
                return View("PostDetails", updatedPost);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError(ex, "Error adding comment");

                // Add a model error and return the view
                ModelState.AddModelError(string.Empty, "An error occurred while adding the comment. Please try again.");
                var post = _postManager.GetPostByIdWithComments(postId);
                return View("PostDetails", post);
            }
        }

        public IActionResult EditComment(int commentId)
        {
            var comment = _commentManager.GetCommentById(commentId);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        [HttpPost]
        public IActionResult UpdateComment(int commentId, string text)
        {
            var updatedComment = _commentManager.UpdateComment(commentId, text);
            if (updatedComment == null)
            {
                return NotFound();
            }

            return RedirectToAction("PostDetails", new { id = updatedComment.PostId });
        }

        public IActionResult DeleteComment(int commentId)
        {
            var deleted = _commentManager.DeleteComment(commentId);
            if (!deleted)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}