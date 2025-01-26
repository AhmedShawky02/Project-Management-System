using Project_Management_System.Dtos.Tasks;
using Project_Management_System.Models;

namespace Project_Management_System.Dtos.Projects
{
    public class ProjectDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int StatusId { get; set; }
        public int CreatedBy { get; set; }
        public List<TaskDto> tasks { get; set; }
    }
}
