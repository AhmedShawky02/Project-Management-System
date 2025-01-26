using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Project_Management_System.Models;
using Project_Management_System.Dtos.Comments;

namespace Project_Management_System.Dtos.Tasks
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime DueDate { get; set; }
        public int ProjectId { get; set; }
        public int AssignedTo { get; set; }
        public ICollection<CommentDto> Comments { get; set; }
    }
}
