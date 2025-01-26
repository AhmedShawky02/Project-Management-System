using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Dtos.Comments
{
    public class CommentUpdateDto
    {
        [Required]
        public string Text { get; set; }
    }
}
