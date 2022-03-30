namespace kanban_api.Authentication
{
    public class TokenConfigurations
    {
        public string Secret { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpirationTimeInHours { get; set; }
    }
}
