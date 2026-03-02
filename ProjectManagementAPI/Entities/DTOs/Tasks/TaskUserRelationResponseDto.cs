namespace EFCoreAPI.Entities.DTOs.Tasks
{
    public class TaskUserRelationResponseDto
    {
        public int Id { get; set; }

        public int TaskId { get; set; }
        public int UserId { get; set; }
    }
}
