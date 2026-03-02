namespace EFCoreAPI.Repositories.Models
{
    public class Profile
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }

        // Navigation
        public ICollection<UserProfileRelation> UserProfiles { get; set; } = new List<UserProfileRelation>();

    }
}
