using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Dtos.Comments
{
    public class CommentCreateDto
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public int TaskId { get; set; }
    }
}
