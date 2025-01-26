using Project_Management_System.Models;

namespace Project_Management_System.Interfaces.Comments_Interface
{
    public interface ICommentRepository
    {
        Task<Comment> CreateCommentAsync(Comment comment);
        Task<List<Comment>> GetAllCommentsAsync();
        Task<Comment> GetCommentById(int id);
        Task<Comment> UpdateCommentAsync(Comment comment);
        Task DeleteTaskAsync(Comment comment);
    }
}
