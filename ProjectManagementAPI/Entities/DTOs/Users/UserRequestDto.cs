namespace EFCoreAPI.Entities.DTOs.Users
{
    public class UserRequestDto
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string DomainName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
