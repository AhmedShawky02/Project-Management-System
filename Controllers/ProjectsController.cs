using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project_Management_System.Dtos.Projects;
using Project_Management_System.Interfaces;
using Project_Management_System.Interfaces.Projects_Interface;
using Project_Management_System.Mapping;
using Project_Management_System.Models;
using System.Security.Claims;

namespace Project_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectsController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IProjectRepository _projectRepo;

        public ProjectsController(IUserRepository userRepo ,
            IProjectRepository projectRepo)
        {
            _userRepo = userRepo;
            _projectRepo = projectRepo;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> AddNewProject([FromBody] ProjectCreateDto projectCreateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var UserName = User.FindFirstValue(ClaimTypes.Name);  

                var UserExists = await _userRepo.GetUserByUserNameAsync(UserName!);

                if (UserExists == null)
                {
                    return Unauthorized("User not found.");
                }

                if (projectCreateDto.StartDate >= projectCreateDto.EndDate)
                {
                    return BadRequest("StartDate must be earlier than EndDate.");
                }

                if (projectCreateDto.StartDate < DateTime.Now)
                {
                    return BadRequest("StartDate cannot be in the past.");
                }

                var project = new Project()
                {
                    Name = projectCreateDto.Name!,
                    Description = projectCreateDto.Description!,
                    StartDate = projectCreateDto.StartDate,
                    EndDate = projectCreateDto.EndDate,
                    Status = 3,
                    CreatedBy = UserExists.UserId
                };

                await _projectRepo.CreateProjectAsync(project);

                return Ok(new { ProjectId = project.ProjectId, Message = "Project created successfully" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                var projects = await _projectRepo.GetAllProjectsAsync();


                if (projects == null || !projects.Any())
                {
                    return NotFound("No projects found.");
                }

                var projectDtos = projects.ToProjectDtoConversion();

                return Ok(projectDtos);

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
                var projectExists = await _projectRepo.GetProjectById(id);

                if (projectExists == null)
                    return NotFound("Project not found.");

                var projectDtos = projectExists.ToProjectDtoConversion();

                return Ok(projectDtos);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject([FromRoute] int id, [FromBody] ProjectUpdateDto updateDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var projectExists = await _projectRepo.GetProjectById(id);

                if (projectExists == null)
                    return NotFound("Project not found.");

                if (updateDto.StartDate >= updateDto.EndDate)
                    return BadRequest("StartDate must be earlier than EndDate.");

                projectExists.Name = updateDto.Name;
                projectExists.Description = updateDto.Description;
                projectExists.StartDate = updateDto.StartDate;
                projectExists.EndDate = updateDto.EndDate;
                projectExists.Status = updateDto.StatusId;

                await _projectRepo.UpdateProjectAsync(projectExists);

                return Ok(new { Message = "Project updated successfully." });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject([FromRoute] int id)
        {
            try
            {
                var projectExists = await _projectRepo.GetProjectById(id);

                if (projectExists == null)
                {
                    return NotFound(new { Message = "Project not found." });
                }

                if(projectExists.Tasks.Any())
                {
                    return BadRequest(new { Message = "Please delete all Tasks associated with this task before deleting the project." });
                }

                await _projectRepo.DeleteProjectAsync(projectExists);
                return Ok(new { Message = "Project deleted successfully." });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
