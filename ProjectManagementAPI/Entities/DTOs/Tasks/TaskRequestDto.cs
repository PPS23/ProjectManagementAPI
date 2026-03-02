
using EFCoreAPI.Entities.DTOs.Users;

namespace EFCoreAPI.Entities.DTOs.Tasks
{
    public class TaskRequestDto
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public List<TaskUserRelationRequestDto> ResponsableUsers { get; set; }
    }
}
