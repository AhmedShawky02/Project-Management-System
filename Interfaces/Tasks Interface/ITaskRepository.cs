using Project_Management_System.Models;

namespace Project_Management_System.Interfaces.Tasks_Interface
{
    public interface ITaskRepository
    {
        Task<TaskModel> CreateTaskAsync(TaskModel task);

        Task<List<TaskModel>> GetAllTasksAsync();

        Task<TaskModel> GetTaskById(int id);

        Task<TaskModel> UpdateTaskAsync(TaskModel task);

        Task DeleteTaskAsync(TaskModel task);

    }
}
