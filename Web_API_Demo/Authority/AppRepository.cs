namespace Web_API_Demo.Authority
{
    public static class AppRepository
    {
        private static List<Application> _applications = new List<Application>()
        {
            new Application
            {
                ApplicationId = 1,
                ApplicationName = "MVCWebApp",
                ClientId = "53D3C1E6-1234-4QWE-ASAFJ1689AJ1K1",
                Secret = "ASDAD1AS-12AS-123A-123ASDAD12ASDA",
                Scopes = "read,write"
            }
        };

        public static bool Authenticate(string clientId, string secret)
        {
            return _applications.Any(x => x.ClientId == clientId && x.Secret == secret);
        }

        public static Application? GetApplicationByClientId(string clientId)
        {
            return _applications.FirstOrDefault(x => x.ClientId == clientId);
        }
    }
}
