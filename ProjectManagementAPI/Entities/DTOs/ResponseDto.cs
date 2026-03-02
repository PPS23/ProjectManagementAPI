namespace EFCoreAPI.Entities.DTOs
{
    public class ResponseDto
    {
        public ResponseDto() { }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
