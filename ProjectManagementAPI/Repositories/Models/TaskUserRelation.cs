namespace EFCoreAPI.Repositories.Models
{
    public class TaskUserRelation
    {
        public int Id { get; set; }

        public int TaskId { get; set; }
        public int UserId { get; set; }

        // Navigation
        public TaskModel Task { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
