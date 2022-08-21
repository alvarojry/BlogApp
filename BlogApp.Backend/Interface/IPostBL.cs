using BlogApp.Common.Model.API;
using BlogApp.Common.Model.Blog;
using System.Collections.Generic;

namespace BlogApp.Backend.Interface
{
    public interface IPostBL
    {
        public IEnumerable<Post> GetPublished();

        public IEnumerable<Post> GetOwn(string userId);

        public bool CreateComment(long postId, string userId, Comment comment);

        public bool CreatePost(string userid, ref Post post);

        public (bool, Post) UpdatePost(long postId, string userId, Post post);

        public bool SubmitPost(long postId, string userId);

        public IEnumerable<Post> GetPending();

        public bool ApprovePost(long postId);

        public bool RejectPost(long postId, EditorComment comment);
    }
}