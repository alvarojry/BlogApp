using BlogApp.Common.Model.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Common.Model.Blog
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        public DateTime PublishDate { get; set; }
        public User Author { get; set; }
        [Required]
        public PostStatus Status { get; set; }
        public string EditorComment { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}