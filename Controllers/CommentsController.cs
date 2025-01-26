using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Dtos.Comments;
using Project_Management_System.Dtos.Tasks;
using Project_Management_System.Interfaces;
using Project_Management_System.Interfaces.Comments_Interface;
using Project_Management_System.Interfaces.Tasks_Interface;
using Project_Management_System.Mapping;
using Project_Management_System.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Project_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly ITaskRepository _taskRepo;
        private readonly IUserRepository _userRepo;
        private readonly ICommentRepository _commentRepo;

        public CommentsController(ITaskRepository taskRepo,
            IUserRepository userRepo,
            ICommentRepository commentRepo)
        {
            _taskRepo = taskRepo;
            _userRepo = userRepo;
            _commentRepo = commentRepo;
        }


        [HttpPost("Create")]
        public async Task<IActionResult> AddNewComment([FromBody] CommentCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var task = await _taskRepo.GetTaskById(createDto.TaskId);

                if (task == null)
                    return NotFound("Task not found.");

                var userName = User.FindFirstValue(ClaimTypes.Name);

                var user = await _userRepo.GetUserByUserNameAsync(userName!);

                if (user == null)
                    return Unauthorized("User not found.");

                var comment = new Comment()
                {
                    Text = createDto.Text,
                    CreatedAt = DateTime.UtcNow,
                    TaskId = createDto.TaskId,
                    CreatedBy = user.UserId,
                };

                await _commentRepo.CreateCommentAsync(comment);

                return Ok(new { CommentId = comment.CommentId, Message = "Comment created successfully." });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllComment()
        {
            try
            {
                var comments = await _commentRepo.GetAllCommentsAsync();

                if (comments == null || !comments.Any())
                {
                    return NotFound("No comments found.");
                }

                var CommentsDto = comments.ToCommentDtoConversion();

                return Ok(CommentsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { Message = "Invalid Comment ID." });
                }

                var comment = await _commentRepo.GetCommentById(id);
                if (comment == null)
                {
                    return NotFound(new { Message = "Comment not found." });
                }

                var commentDto = comment.ToCommentDtoConversion();

                return Ok(commentDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCommet ([FromRoute] int id, [FromBody] CommentUpdateDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var comment = await _commentRepo.GetCommentById(id);

                if (comment == null)
                {
                    return NotFound(new { Message = "Comment not found." });
                }

                comment.Text = updateDto.Text;

                await _commentRepo.UpdateCommentAsync(comment);

                return Ok(new { Message = "Comment updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            try
            {
                var comment = await _commentRepo.GetCommentById(id);

                if (comment == null)
                {
                    return NotFound(new { Message = "Comment not found." });
                }

                await _commentRepo.DeleteTaskAsync(comment);

                return Ok(new { Message = "Comment deleted successfully." });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
