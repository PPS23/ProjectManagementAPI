namespace EFCoreAPI.Repositories.Models
{
    public class Project
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        // Navigation
        public ICollection<ProjectAttachmentRelation> ProjectAttachments { get; set; } = new List<ProjectAttachmentRelation>();
        public ICollection<TaskModel> Tasks { get; set; } = new List<TaskModel>();
    }
}
