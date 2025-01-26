using Project_Management_System.Models;

namespace Project_Management_System.Interfaces.Projects_Interface
{
    public interface IProjectRepository
    {
        Task<Project> CreateProjectAsync(Project project);
        Task<List<Project>> GetAllProjectsAsync();
        Task<Project> GetProjectById(int id);
        Task<Project> UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(Project project);

    }
}
