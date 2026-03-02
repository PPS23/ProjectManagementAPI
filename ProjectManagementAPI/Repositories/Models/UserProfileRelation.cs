namespace EFCoreAPI.Repositories.Models
{
    public class UserProfileRelation
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int ProfileId { get; set; }
        public bool IsActive { get; set; }

        // Navigation
        public User User { get; set; } = null!;
        public Profile Profile { get; set; } = null!;
    }
}
