using EFCoreAPI.Entities.DTOs.Users;

namespace EFCoreAPI.Entities.DTOs.Comments
{
    public class CommentRequestDto
    {
        public int Id { get; set; }

        public int TaskId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; } = null!;
    }
}
