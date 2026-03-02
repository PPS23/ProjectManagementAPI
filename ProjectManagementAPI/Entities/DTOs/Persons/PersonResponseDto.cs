namespace EFCoreAPI.Entities.DTOs.Persons
{
    public class PersonResponseDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
    }
}
