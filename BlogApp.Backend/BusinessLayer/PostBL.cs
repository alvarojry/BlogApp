using BlogApp.Backend.Interface;
using BlogApp.Common.Model.API;
using BlogApp.Common.Model.Blog;
using BlogApp.Common.Model.Security;
using System.Collections.Generic;
using System.Linq;

namespace BlogApp.Backend.BusinessLayer
{
    public class PostBL : IPostBL
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public PostBL(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<Post> GetPublished()
        {
            return _postRepository.GetAll().Where(p => p.Status == PostStatus.APPROVED).ToList();
        }

        public bool CreateComment(long postId, string userId, Comment comment)
        {
            Post post = _postRepository.Get(postId);
            User user = GetUser(userId);
            if (post != null && post.Status == PostStatus.APPROVED && user != null)
            {
                comment.Author = user;
                post.Comments.Add(comment);
                return _postRepository.Update(post);
            }
            return false;
        }

        public IEnumerable<Post> GetOwn(string userId)
        {
            User user = GetUser(userId);
            if (user != null)
            {
                return _postRepository.GetAll().Where(p => p.Author == user).ToList();
            }
            return null;
        }

        public bool CreatePost(string userId, ref Post post)
        {
            User user = GetUser(userId);
            if (post != null && !string.IsNullOrWhiteSpace(post.Text) && user != null)
            {
                post.Author = user;
                post.PublishDate = System.DateTime.Now;
                post.Status = PostStatus.CREATED;
                return _postRepository.Insert(post);
            }
            return false;
        }

        public (bool, Post) UpdatePost(long postId, string userId, Post post)
        {
            Post dbPost = _postRepository.Get(postId);
            User user = GetUser(userId);
            if (dbPost != null && user != null && dbPost.Author == user && dbPost.Status == PostStatus.CREATED)
            {
                dbPost.Title = post.Title;
                dbPost.Text = post.Text;
                dbPost.PublishDate = System.DateTime.Now;
                bool saved = _postRepository.Update(dbPost);
                return (saved, dbPost);
            }

            return (false, null);
        }

        public bool SubmitPost(long postId, string userId)
        {
            Post post = _postRepository.Get(postId);
            User user = GetUser(userId);
            if (post != null && user != null && post.Author == user && post.Status == PostStatus.CREATED)
            {
                post.Status = PostStatus.SUBMITTED;
                post.PublishDate = System.DateTime.Now;
                return _postRepository.Update(post);
            }
            return false;
        }

        public IEnumerable<Post> GetPending()
        {
            return _postRepository.GetAll().Where(p => p.Status == PostStatus.SUBMITTED).ToList();
        }

        public bool ApprovePost(long postId)
        {
            Post post = _postRepository.Get(postId);
            if (post != null && post.Status == PostStatus.SUBMITTED)
            {
                post.Status = PostStatus.APPROVED;
                post.PublishDate = System.DateTime.Now;
                post.EditorComment = null;
                return _postRepository.Update(post);
            }
            return false;
        }

        public bool RejectPost(long postId, EditorComment comment)
        {
            Post post = _postRepository.Get(postId);
            if (post != null && post.Status == PostStatus.SUBMITTED)
            {
                post.Status = PostStatus.CREATED;
                post.PublishDate = System.DateTime.Now;
                post.EditorComment = comment.Comment;
                return _postRepository.Update(post);
            }
            return false;
        }

        private User GetUser(string userId)
        {
            bool ok = long.TryParse(userId, out long lUserId);
            if (ok)
            {
                return _userRepository.Get(lUserId);
            }
            return null;
        }

    }
}