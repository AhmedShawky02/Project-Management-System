using Microsoft.EntityFrameworkCore;
using Project_Management_System.Context;
using Project_Management_System.Interfaces.Tasks_Interface;
using Project_Management_System.Models;

namespace Project_Management_System.Repositories.Tasks_Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ProjectManagementDbContext _context;

        public TaskRepository(ProjectManagementDbContext context)
        {
            _context = context;
        }
        public async Task<TaskModel> CreateTaskAsync(TaskModel task)
        {
            await _context.Tasks.AddAsync(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task DeleteTaskAsync(TaskModel task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TaskModel>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskModel> GetTaskById(int id)
        {
            var Task = await _context.Tasks.FirstOrDefaultAsync(x => x.TaskId == id);
            if (Task == null)
            {
                return null;
            }
            return Task;
        }

        public async Task<TaskModel> UpdateTaskAsync(TaskModel task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }
    }
}
