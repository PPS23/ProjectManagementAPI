using EFCoreAPI.Entities.DTOs.Users;
using EFCoreAPI.Repositories.Models;

namespace EFCoreAPI.Entities.DTOs.Tasks
{
    public class TaskUserRelationRequestDto
    {
        public int Id { get; set; }

        public int TaskId { get; set; }
        public int UserId { get; set; }
    }
}
