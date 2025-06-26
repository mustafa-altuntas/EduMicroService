namespace EduMicroService.Catalog.Api.Options

{
    public class CustomTokenOption
    {
        public List<String> Audiences { get; set; }
        public string Issuer { get; set; } = default!;

        public double AccessTokenExpiration { get; set; }
        public double RefreshTokenExpiration { get; set; }

        public string SecurityKey { get; set; } = default!;
    }
}
