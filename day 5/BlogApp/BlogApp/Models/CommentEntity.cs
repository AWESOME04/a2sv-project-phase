using System;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class CommentEntity
    {
        [Key]
        public int CommentId { get; set; }

        [Required]
        public int PostId { get; set; }

        [Required]
        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public PostEntity Post { get; set; }
    }
}
