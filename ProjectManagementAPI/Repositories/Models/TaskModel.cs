namespace EFCoreAPI.Repositories.Models
{
    public class TaskModel
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }

        // Navigation
        public Project Project { get; set; } = null!;
        public Status Status { get; set; } = null!;
        public ICollection<TaskUserRelation> TaskUsers { get; set; } = new List<TaskUserRelation>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
