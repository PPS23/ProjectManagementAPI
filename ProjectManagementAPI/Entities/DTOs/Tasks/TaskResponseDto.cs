using EFCoreAPI.Entities.DTOs.Comments;
using EFCoreAPI.Entities.DTOs.Projects;
using EFCoreAPI.Entities.DTOs.Status;
using EFCoreAPI.Entities.DTOs.Users;
using EFCoreAPI.Repositories.Models;

namespace EFCoreAPI.Entities.DTOs.Tasks
{
    public class TaskResponseDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

        public ProjectResponseDto Project { get; set; } = null!;
        public StatusResponseDto Status { get; set; } = null!;
        public ICollection<UserResponseDto> TaskUsers { get; set; } = new List<UserResponseDto>();
        public ICollection<CommentResponseDto> Comments { get; set; } = new List<CommentResponseDto>();
    }
}
