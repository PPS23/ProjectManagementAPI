using EFCoreAPI.Entities.DTOs.Persons;

namespace EFCoreAPI.Entities.DTOs.Users
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string DomainName { get; set; }
        public bool IsActive { get; set; }
        public PersonResponseDto Person { get; set; }
    }
}
