namespace EFCoreAPI.Entities.DTOs.Projects
{
    public class ProjectResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
