namespace EduMicroService.Identity.Api.Options
{
    public class ClientTokenOption
    {
        public string ClientId { get; set; } = default!;
        public string ClientSecret { get; set; } = default!;
        public List<string> Audiences { get; set; } = new();
    }
}
