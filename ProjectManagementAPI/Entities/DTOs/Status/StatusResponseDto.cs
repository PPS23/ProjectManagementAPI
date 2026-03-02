namespace EFCoreAPI.Entities.DTOs.Status
{
    public class StatusResponseDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}
