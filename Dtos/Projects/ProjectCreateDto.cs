using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Dtos.Projects
{
    public class ProjectCreateDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
