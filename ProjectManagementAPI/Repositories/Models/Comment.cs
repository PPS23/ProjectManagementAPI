namespace EFCoreAPI.Repositories.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int TaskId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }

        // Navigation
        public TaskModel Task { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
