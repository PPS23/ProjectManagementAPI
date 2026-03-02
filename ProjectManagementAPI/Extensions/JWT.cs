namespace EFCoreAPI.Extensions
{
    public class JWT
    {
        public string Key { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpiresInMinutes { get; set; }
    }
}
