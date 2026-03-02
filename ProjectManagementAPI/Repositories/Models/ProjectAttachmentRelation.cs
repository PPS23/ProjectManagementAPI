namespace EFCoreAPI.Repositories.Models
{
    public class ProjectAttachmentRelation
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }
        public int AttachmentId { get; set; }

        // Navigation
        public Project Project { get; set; } = null!;
        public Attachment Attachment { get; set; } = null!;
    }
}
