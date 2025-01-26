using Project_Management_System.Models;

namespace Project_Management_System.Interfaces
{
    public interface ITokenRepository
    {
        Task<string> CreateToken(User user);
    }
}
