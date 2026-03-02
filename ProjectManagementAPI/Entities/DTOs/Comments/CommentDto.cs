using EFCoreAPI.Entities.DTOs.Users;

namespace EFCoreAPI.Entities.DTOs.Comments
{
    public class CommentDto
    {
        public int Id { get; set; }

        public int TaskId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public string Description { get; set; } = null!;
        public UserResponseDto User { get; set; }
    }
}
