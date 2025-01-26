using Project_Management_System.Models;

namespace Project_Management_System.Interfaces
{
    public interface IUserRoleRepository
    {
        Task AddUserRoleAsync(UserRole userRole);

        Task<List<UserRole>> GetUserRolesByUserId(int  userId);
    }
}
