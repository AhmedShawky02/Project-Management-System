using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Dtos.Projects;
using Project_Management_System.Dtos.Tasks;
using Project_Management_System.Interfaces;
using Project_Management_System.Interfaces.Projects_Interface;
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
    public class TasksController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly ITaskRepository _taskRepo;

        public TasksController(IUserRepository userRepo ,
            IProjectRepository projectRepo ,
            ITaskRepository taskRepo)
        {
            _userRepo = userRepo;
            _projectRepo = projectRepo;
            _taskRepo = taskRepo;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AddNewTask([FromBody] TaskCreateDto createDto)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var UserName = User.FindFirstValue(ClaimTypes.Name);

                var UserExists = await _userRepo.GetUserByUserNameAsync(UserName!);

                if (UserExists == null)
                {
                    return Unauthorized("User not found.");
                }

                if (createDto.DueDate < DateTime.Now)
                {
                    return BadRequest("DueDate cannot be in the past.");
                }

                var Project = await _projectRepo.GetProjectById(createDto.ProjectId);

                if (Project == null)
                {
                    return NotFound("Project not found.");
                }

                var Task = new TaskModel()
                {
                    Name = createDto.Name,
                    Description = createDto.Description,
                    Status = 3,
                    DueDate = createDto.DueDate,
                    ProjectId = createDto.ProjectId,
                    AssignedTo = UserExists.UserId,
                };

                await _taskRepo.CreateTaskAsync(Task);

                return Ok(new { TaskId = Task.TaskId, Message = "Task created successfully" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var Tasks = await _taskRepo.GetAllTasksAsync();

                if (Tasks == null || !Tasks.Any())
                {
                    return NotFound("No Tasks found.");
                }

                var TasksDto = Tasks.ToTaskDtoConversion();

                return Ok(TasksDto);

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
                var task = await _taskRepo.GetTaskById(id);

                if (task == null)
                {
                    return NotFound(new { Message = "Task not found." });
                }

                var taskDto = task.ToTaskDtoConversion();

                return Ok(taskDto);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask([FromRoute] int id, [FromBody] TaskUpdateDto taskUpdate)
        {
            try
            {
                var task = await _taskRepo.GetTaskById(id);

                if (task == null)
                {
                    return NotFound(new { Message = "Task not found." });
                }

                var project = await _projectRepo.GetProjectById(taskUpdate.ProjectId);

                if (project == null)
                {
                    return NotFound(new { Message = "Project not found." });
                }

                if (taskUpdate.DueDate < DateTime.Now)
                {
                    return BadRequest("DueDate cannot be in the past.");
                }

                if (taskUpdate.Status != 1 && taskUpdate.Status != 2 && taskUpdate.Status != 3)
                {
                    return BadRequest("Status must be one of the following: 1) Complete, 2) In Progress, 3) Pending.");
                }

                task.Name = taskUpdate.Name;
                task.Description = taskUpdate.Description;
                task.Status = taskUpdate.Status;
                task.DueDate = taskUpdate.DueDate;
                task.ProjectId = taskUpdate.ProjectId;

                await _taskRepo.UpdateTaskAsync(task);

                return Ok(new { Message = "Task updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask([FromRoute] int id)
        {
            try
            {
                var task = await _taskRepo.GetTaskById(id);

                if (task == null)
                {
                    return NotFound(new { Message = "Task not found." });
                }

                if (task.Comments.Any())
                {
                    return BadRequest(new { Message = "Please delete all comments associated with this task before deleting the task." });
                }

                await _taskRepo.DeleteTaskAsync(task);
                return Ok(new { Message = "Task deleted successfully." });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
