namespace EFCoreAPI.Repositories.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public bool IsActive { get; set; }

        // Navigation
        public User? User { get; set; }
    }
}
