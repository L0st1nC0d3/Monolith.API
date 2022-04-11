using Business;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Monolith.Models;

namespace Monolith.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly EntitiesManager _entitiesManager;

        public HomeController(EntitiesManager entitiesManager)
        {
            _entitiesManager = entitiesManager;
        }

        #region Users
        [HttpPost("users")]
        [ProducesResponseType(typeof(int), 200)]
        public ActionResult<int> AddUser([FromBody] string userName)
        {
            return Ok(_entitiesManager.AddNewUser(userName));
        }

        [HttpGet("users/{id:int}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        public ActionResult<UserDto> GetUserByUserId([FromRoute] int id)
        {
            var user = _entitiesManager.GetUserById(id);
            return Ok(user);
        }

        [HttpDelete("users/{id:int}")]
        [ProducesResponseType(typeof(int), 200)]
        public ActionResult<int> DeleteUser([FromRoute] int id)
        {
            return Ok(_entitiesManager.DeleteUser(id));
        }
        #endregion

        #region Posts
        [HttpPost("posts")]
        [ProducesResponseType(typeof(int), 200)]
        public ActionResult<int> AddPost([FromBody] CreatePost post)
        {
            var user = _entitiesManager.GetUserById(post.AuthorId);
            if (user == null) return BadRequest();
            return Ok(_entitiesManager.AddNewPost(user.UserName, post.Title, post.Text));
        }

        [HttpGet("posts/{userName}")]
        [ProducesResponseType(typeof(List<PostDto>), 200)]
        public ActionResult<List<PostDto>> GetPostsByAuthor([FromRoute] string userName)
        {
            return Ok(_entitiesManager.GetAllPostsByUserName(userName));
        }

        [HttpGet("posts/{id:int}")]
        [ProducesResponseType(typeof(PostDto), 200)]
        public ActionResult<PostDto> GetPostById([FromRoute] int id)
        {
            var result = _entitiesManager.GetPostById(id);
            return Ok(result);
        }

        [HttpPut("posts/{id:int}")]
        [ProducesResponseType(typeof(int), 200)]
        public ActionResult<int> EditPost([FromRoute] int id, [FromBody] EditPost newPost)
        {
            return Ok(_entitiesManager.EditPost(id, newPost.Title, newPost.Text));
        }

        [HttpDelete("posts/{id:int}")]
        [ProducesResponseType(typeof(int), 200)]
        public ActionResult<int> DeletePost([FromRoute] int id)
        {
            return Ok(_entitiesManager.DeletePost(id));
        }
        #endregion

        #region Comments
        [HttpPost("comments")]
        [ProducesResponseType(typeof(int), 200)]
        public ActionResult<int> AddComment([FromBody] Comment comment)
        {
            return Ok(_entitiesManager.AddNewComment(comment.PostId, comment.AuthorId, comment.Text));
        }

        [HttpGet("comments/{id:int}")]
        [ProducesResponseType(typeof(CommentDto), 200)]
        public ActionResult<List<CommentDto>> GetCommentById([FromRoute] int id)
        {
            return Ok(_entitiesManager.GetCommentById(id));
        }

        [HttpPut("comments/{id:int}")]
        [ProducesResponseType(typeof(int), 200)]
        public ActionResult<int> EditComment([FromRoute] int id, [FromBody] string newText)
        {
            return Ok(_entitiesManager.EditComment(id, newText));
        }

        [HttpDelete("comments/{id:int}")]
        [ProducesResponseType(typeof(int), 200)]
        public ActionResult<int> DeleteComment([FromRoute] int id)
        {
            return Ok(_entitiesManager.DeleteComment(id));
        }
        #endregion
    }
}
