using Project_Management_System.Dtos.Comments;
using Project_Management_System.Models;

namespace Project_Management_System.Mapping
{
    public static class ToCommentDto
    {
        public static CommentDto ToCommentDtoConversion(this Comment comment)
        {
            return new CommentDto()
            {
                CommentId = comment.CommentId,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt,
                TaskId = comment.TaskId,
                CreatedBy = comment.CreatedBy,
            };
        }

        public static IEnumerable<CommentDto> ToCommentDtoConversion(this IEnumerable<Comment> comments)
        {
            return comments.Select(c => c.ToCommentDtoConversion());
        }

    }
}
