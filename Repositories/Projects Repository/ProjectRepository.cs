using Microsoft.EntityFrameworkCore;
using Project_Management_System.Context;
using Project_Management_System.Interfaces.Projects_Interface;
using Project_Management_System.Models;

namespace Project_Management_System.Repositories.Projects_Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ProjectManagementDbContext _context;

        public ProjectRepository(ProjectManagementDbContext context)
        {
            _context = context;
        }
        public async Task<Project> CreateProjectAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task DeleteProjectAsync(Project project)
        {
            _context.Projects.Remove(project);  
            await _context.SaveChangesAsync();  
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<Project> GetProjectById(int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(x => x.ProjectId == id);
            if (project == null)
            {
                return null;
            }
            return project;
        }

        public async Task<Project> UpdateProjectAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return project;
        }
    }
}
