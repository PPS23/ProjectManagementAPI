namespace EFCoreAPI.Repositories.Models
{
    public class Status
    {
        public int Id { get; set; }

        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }

        // Navigation
        public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
    }
}
