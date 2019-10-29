using System.Threading.Tasks;
using ESLearn.API.DTOs;
using ESLearn.Domain.AggregatesModel.PostsAggregate;
using ESLearn.Domain.AggregatesModel.UsersAggregate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ESLearn.API.Controllers
{
    
    [Route("/api/v1/[controller]")]
    public class PostsController: ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostsRepository _postsRepository;
        private readonly ILogger<PostsController> _logger;

        public PostsController(IUserRepository userRepository, IPostsRepository postsRepository, ILogger<PostsController> logger)
        {
            _userRepository = userRepository;
            _postsRepository = postsRepository;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetPost(string id)
        {
            _logger.LogInformation($"the post with id {id} was fetched");
            var post = await _postsRepository.QueryAsync(id);
            return Ok(post);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddPost([FromBody] CreatePostDto postDto)
        {
            var user = await _userRepository.QueryAsync(postDto.UserId);
            var post = new Post(user, postDto.Title, postDto.Content);
            await _postsRepository.IndexAsync(post);
            return Ok(post);
        }
        
        [HttpPost("add/comment")]
        public async Task<IActionResult> AddComment([FromBody] AddCommentDto commentDto)
        {
            var post = await _postsRepository.QueryAsync(commentDto.PostId);
            var comment = new Comment(post, commentDto.Comment);
            await _postsRepository.AddCommentAsync(post, comment);
            return Ok(post);
        }
        
    }
}