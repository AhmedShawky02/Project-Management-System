using Project_Management_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Dtos.Tasks
{
    public class TaskUpdateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int Status { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public int ProjectId { get; set; }
    }
}
