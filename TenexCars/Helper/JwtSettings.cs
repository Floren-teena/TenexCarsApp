namespace TenexCars.Helper
{
    public class JwtSettings
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SigningKey { get; set; }
        public int TokenValidityInMinutes { get; set; }
    }
}
