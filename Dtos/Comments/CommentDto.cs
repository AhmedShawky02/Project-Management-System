using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Dtos.Comments
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public string Text { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int TaskId { get; set; }
        public int CreatedBy { get; set; }
    }
}
