using Project_Management_System.Dtos.Comments;
using Project_Management_System.Dtos.Projects;
using Project_Management_System.Dtos.Tasks;
using Project_Management_System.Models;

namespace Project_Management_System.Mapping
{
    public static class ToProjectDto
    {
        public static ProjectDto ToProjectDtoConversion(this Project project)
        {
            return new ProjectDto()
            {
                Id = project.ProjectId,
                Name = project.Name,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                StatusId = project.Status,
                CreatedBy = project.CreatedBy,
                tasks = project.Tasks.Select(x => new TaskDto()
                {
                    TaskId = x.TaskId,
                    Name = x.Name,
                    Description = x.Description,
                    Status = x.Status,
                    DueDate = x.DueDate,
                    ProjectId = x.ProjectId,
                    AssignedTo = x.AssignedTo,
                    Comments = x.Comments.Select(c => new CommentDto()
                    {
                        CommentId = c.CommentId,
                        Text = c.Text,
                        CreatedAt = c.CreatedAt,
                        TaskId = c.TaskId,
                        CreatedBy = c.CreatedBy,
                    }).ToList()
                }).ToList(),
            };

        }
        public static IEnumerable<ProjectDto> ToProjectDtoConversion(this IEnumerable<Project> projects)
        {
            return projects.Select(project => project.ToProjectDtoConversion());
        }


    }
}
