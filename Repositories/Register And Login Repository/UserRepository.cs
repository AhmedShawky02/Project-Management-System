using Microsoft.EntityFrameworkCore;
using Project_Management_System.Context;
using Project_Management_System.Controllers;
using Project_Management_System.Interfaces;
using Project_Management_System.Models;

namespace Project_Management_System.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ProjectManagementDbContext _context;

        public UserRepository(ProjectManagementDbContext context)
        {
            _context = context;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var UserExists = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (UserExists == null)
            {
                return null;
            }
            return UserExists;
        }

        public async Task<User> GetUserByUserNameAsync(string UserName)
        {
            var UserExists = await _context.Users.FirstOrDefaultAsync(u => u.UserName == UserName);
            if (UserExists == null)
            {
                return null;
            }
            return UserExists;
        }
    }
}
