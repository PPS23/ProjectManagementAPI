namespace EFCoreAPI.Repositories.Models
{
    public class User
    {
        public int Id { get; set; }

        public int PersonId { get; set; }
        public string DomainName { get; set; } = null!;
        public string Salt { get; set; } = string.Empty;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime? LastLogin { get; set; }

        // Navigation
        public Person Person { get; set; } = null!;
        public ICollection<UserProfileRelation> UserProfiles { get; set; } = new List<UserProfileRelation>();
        public ICollection<TaskUserRelation> TaskUsers { get; set; } = new List<TaskUserRelation>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
