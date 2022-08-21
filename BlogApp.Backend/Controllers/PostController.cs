using BlogApp.Backend.Entities;
using BlogApp.Backend.Interface;
using BlogApp.Common.Model.API;
using BlogApp.Common.Model.Blog;
using BlogApp.Common.Model.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BlogApp.Backend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostBL _postBL;

        public PostController(IPostBL postBL)
        {
            _postBL = postBL;
        }

        [Authorize(Roles = nameof(Role.WRITER)+","+nameof(Role.PUBLIC)+","+nameof(Role.EDITOR))]
        [HttpGet]
        [Route("published")]
        public IEnumerable<Post> GetPublished()
        {
            return _postBL.GetPublished();
        }

        [Authorize(Roles = nameof(Role.WRITER) + "," + nameof(Role.PUBLIC) + "," + nameof(Role.EDITOR))]
        [HttpPost]
        [Route("{id}/comment")]
        public IActionResult Comment(long id, [FromBody] Comment comment)
        {
            string userId = User.FindFirst(Constants.UserId).Value;
            if (_postBL.CreateComment(id, userId, comment))
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(Roles = nameof(Role.WRITER))]
        [HttpGet]
        [Route("own")]
        public IEnumerable<Post> GetOwn()
        {
            string userId = User.FindFirst(Constants.UserId).Value;
            return _postBL.GetOwn(userId);
        }

        [Authorize(Roles = nameof(Role.WRITER))]
        [HttpPost]
        public IActionResult Post([FromBody] Post post)
        {
            string userId = User.FindFirst(Constants.UserId).Value;
            if (_postBL.CreatePost(userId, ref post))
            {
                return Ok(post);
            }
            return BadRequest();
        }

        [Authorize(Roles = nameof(Role.WRITER))]
        [HttpPut]
        [Route("{postId}")]
        public IActionResult Put(long postId, [FromBody] Post post)
        {
            string userId = User.FindFirst(Constants.UserId).Value;
            (bool updated, Post dbPost) = _postBL.UpdatePost(postId, userId, post);
            if(updated)
            {
                return Ok(dbPost);
            }
            return BadRequest();
        }

        [Authorize(Roles = nameof(Role.WRITER))]
        [HttpPut]
        [Route("{postId}/submit")]
        public IActionResult Submit(long postId)
        {
            string userId = User.FindFirst(Constants.UserId).Value;            
            if (_postBL.SubmitPost(postId, userId))
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(Roles = nameof(Role.EDITOR))]
        [HttpGet]
        [Route("pending")]
        public IEnumerable<Post> GetPending()
        {
            return _postBL.GetPending();
        }

        [Authorize(Roles = nameof(Role.EDITOR))]
        [HttpPut]
        [Route("{postId}/approve")]
        public IActionResult Approve(long postId)
        {
            if (_postBL.ApprovePost(postId))
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(Roles = nameof(Role.EDITOR))]
        [HttpPut]
        [Route("{postId}/reject")]
        public IActionResult RejectPost(long postId, [FromBody] EditorComment comment)
        {
            if (_postBL.RejectPost(postId, comment))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}