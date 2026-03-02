namespace EFCoreAPI.Repositories.Models
{
    public class Attachment
    {
        public int Id { get; set; }

        public string DisplayName { get; set; } = null!;
        public string Url { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        // Navigation
        public ICollection<ProjectAttachmentRelation> ProjectAttachments { get; set; } = new List<ProjectAttachmentRelation>();

    }
}
