using Project_Management_System.Dtos.Comments;
using Project_Management_System.Dtos.Tasks;
using Project_Management_System.Models;

namespace Project_Management_System.Mapping
{
    public static class ToTaskDto
    {
        public static TaskDto ToTaskDtoConversion(this TaskModel taskModel)
        {
            return new TaskDto()
            {
                TaskId = taskModel.TaskId,
                Name = taskModel.Name,
                Description = taskModel.Description,
                Status = taskModel.Status,
                DueDate = taskModel.DueDate,
                ProjectId = taskModel.ProjectId,
                AssignedTo = taskModel.AssignedTo,

                Comments = taskModel.Comments.Select(c => new CommentDto()
                {
                    CommentId = c.CommentId,
                    Text = c.Text,
                    CreatedAt = c.CreatedAt,
                    TaskId = c.TaskId,
                    CreatedBy = c.CreatedBy
                }).ToList()
            };
        }

        public static IEnumerable<TaskDto> ToTaskDtoConversion(this IEnumerable<TaskModel> tasks)
        {
            return tasks.Select(t => t.ToTaskDtoConversion());
        }
    }
}
