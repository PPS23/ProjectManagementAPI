namespace EFCoreAPI.Entities.DTOs.Users
{
    public class TaskUserRelationRequestDto
    {
        public int Id { get; set; }

        public int TaskId { get; set; }
        public int UserId { get; set; }
    }
}
