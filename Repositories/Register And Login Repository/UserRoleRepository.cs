using Microsoft.EntityFrameworkCore;
using Project_Management_System.Context;
using Project_Management_System.Interfaces;
using Project_Management_System.Models;

namespace Project_Management_System.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ProjectManagementDbContext _context;

        public UserRoleRepository(ProjectManagementDbContext context)
        {
            _context = context;
        }
        public async Task AddUserRoleAsync(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserRole>> GetUserRolesByUserId(int userId)
        {
            return await _context.UserRoles
                    .Where(x => x.UserId == userId)
                    .ToListAsync();
        }
    }
}
