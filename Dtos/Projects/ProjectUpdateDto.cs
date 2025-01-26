using System.ComponentModel.DataAnnotations;

namespace Project_Management_System.Dtos.Projects
{
    public class ProjectUpdateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int StatusId { get; set; }
    }
}
