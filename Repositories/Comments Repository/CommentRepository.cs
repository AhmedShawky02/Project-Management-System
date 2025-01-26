using Microsoft.EntityFrameworkCore;
using Project_Management_System.Context;
using Project_Management_System.Interfaces.Comments_Interface;
using Project_Management_System.Models;

namespace Project_Management_System.Repositories.Comments_Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ProjectManagementDbContext _context;

        public CommentRepository(ProjectManagementDbContext context)
        {
            _context = context;
        }
        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task DeleteTaskAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<Comment> GetCommentById(int id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == id);
           if (comment == null)
            {
                return null;
            }
            return comment;
              
        }

        public async Task<Comment> UpdateCommentAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
            return comment;
        }
    }
}
