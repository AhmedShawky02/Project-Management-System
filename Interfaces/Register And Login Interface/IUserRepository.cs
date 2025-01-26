using Project_Management_System.Models;

namespace Project_Management_System.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserByUserNameAsync(string UserName);
    }
}
